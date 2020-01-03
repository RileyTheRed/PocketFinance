using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using PocketFinance.Models;
using PocketFinance.Utilities;
using Xamarin.Forms;

namespace PocketFinance.ViewModels
{
    public class SelectRecordPageViewModel : INotifyPropertyChanged
    {

        #region Properties
        SelectRecordPage parentPage;
        Record record;

        private string _amount;
        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Amount"));
            }
        }

        private int _selectedCategoryIndex;
        public int SelectedCategoryIndex
        {
            get { return _selectedCategoryIndex; }
            set
            {
                _selectedCategoryIndex = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedCategoryIndex"));
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Date"));
            }
        }

        private bool _expenseChecked;
        public bool ExpenseChecked
        {
            get { return _expenseChecked; }
            set
            {
                _expenseChecked = value;
                if (value == true)
                {
                    IncomeChecked = false;
                }
                GetAvailableCategories(value, IncomeChecked);
                PropertyChanged(this, new PropertyChangedEventArgs("ExpenseChecked"));
            }
        }

        private bool _incomeChecked;
        public bool IncomeChecked
        {
            get { return _incomeChecked; }
            set
            {
                _incomeChecked = value;
                if (value == true)
                {
                    ExpenseChecked = false;
                }
                GetAvailableCategories(ExpenseChecked, value);
                PropertyChanged(this, new PropertyChangedEventArgs("IncomeChecked"));
            }
        }

        private bool _submitEnabled;
        public bool SubmitEnabled
        {
            get { return _submitEnabled; }
            set
            {
                _submitEnabled = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SubmitEnabled"));
            }
        }

        private List<string> _availCategories;
        public List<string> AvailCategories
        {
            get { return _availCategories; }
            set
            {
                _availCategories = value;
                PropertyChanged(this, new PropertyChangedEventArgs("AvailCategories"));
            }
        }

        private string _amountColor;
        public string AmountColor
        {
            get { return _amountColor; }
            set
            {
                _amountColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("AmountColor"));
            }
        }

        private string _categoryColor;
        public string CategoryColor
        {
            get { return _categoryColor; }
            set
            {
                _categoryColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CategoryColor"));
            }
        }

        public DateTime MaxDateValue
        {
            get { return DateTime.Now; }
        }
        #endregion

        #region Commands
        public ICommand ExpenseCheckedChanged
        {
            get
            {
                if (_expenseCheckedChanged == null)
                {
                    _expenseCheckedChanged = new DelegateCommand(ExpenseCheckChanged);
                }
                return _expenseCheckedChanged;
            }
        }
        DelegateCommand _expenseCheckedChanged;
        public void ExpenseCheckChanged(object obj)
        {
            //if (!ExpenseChecked && !IncomeChecked)
            //{
            //    SubmitEnabled = false;
            //}
            //else if (!ExpenseChecked && record.RecordType.Equals("expense"))
            //{
            //    SubmitEnabled = true;
            //}
            //else
            //{
            //    SubmitEnabled = true;
            //}
        }

        public ICommand IncomeCheckedChanged
        {
            get
            {
                if (_incomeCheckedChanged == null)
                {
                    _incomeCheckedChanged = new DelegateCommand(IncomeCheckChanged);
                }
                return _incomeCheckedChanged;
            }
        }
        DelegateCommand _incomeCheckedChanged;
        public void IncomeCheckChanged(object obj)
        {
            //if (!ExpenseChecked && !IncomeChecked)
            //{
            //    SubmitEnabled = false;
            //}
            //else if (!IncomeChecked && record.RecordType.Equals("income"))
            //{
            //    SubmitEnabled = true;
            //}
            //else
            //{
            //    SubmitEnabled = true;
            //}
        }

        public ICommand AmountLostFocus
        {
            get
            {
                if (_amountLostFocus == null)
                {
                    _amountLostFocus = new DelegateCommand(LostFocusAmount);
                }
                return _amountLostFocus;
            }
        }
        DelegateCommand _amountLostFocus;
        public void LostFocusAmount(object obj)
        {
            try
            {
                _ = Double.Parse(Amount);
                AmountColor = "Wheat";
            }
            catch
            {
                AmountColor = "Salmon";
            }
        }

        public ICommand CategoryLostFocus
        {
            get
            {
                if (_categoryLostFocus == null)
                {
                    _categoryLostFocus = new DelegateCommand(LostFocusCategory);
                }
                return _categoryLostFocus;
            }
        }
        DelegateCommand _categoryLostFocus;
        public void LostFocusCategory(object obj)
        {
            if (SelectedCategoryIndex == -1)
            {
                CategoryColor = "Salmon";
            }
            else
            {
                CategoryColor = "Wheat";
            }
        }

        public ICommand BackClickedCommand
        {
            get
            {
                if (_backClickedCommand == null)
                {
                    _backClickedCommand = new DelegateCommand(BackButtonClicked);
                }
                return _backClickedCommand;
            }
        }
        DelegateCommand _backClickedCommand;
        public void BackButtonClicked(object obj)
        {
            parentPage.parentPage.vm.Refresh();
            Application.Current.MainPage = parentPage.parentPage;
        }

        public ICommand DeleteClickedCommand
        {
            get
            {
                if (_deleteClickedCommand == null)
                {
                    _deleteClickedCommand = new DelegateCommand(DeleteButtonClicked);
                }
                return _deleteClickedCommand;
            }
        }
        DelegateCommand _deleteClickedCommand;
        async public void DeleteButtonClicked(object obj)
        {
            var response =
                await parentPage.DisplayAlert("Confirm", "Are you sure you want to delete the record?",
                "Yes", "No");
            if (response)
            {
                record.IsDeleted = true;
                parentPage.parentPage.vm.Refresh();
                Application.Current.MainPage = parentPage.parentPage;
            }
            else
            {
                Application.Current.MainPage = parentPage.parentPage;
            }

        }

        public ICommand SubmitClickedCommand
        {
            get
            {
                if (_submitClickedCommand == null)
                {
                    _submitClickedCommand = new DelegateCommand(SubmitButtonClicked);
                }
                return _submitClickedCommand;
            }
        }
        DelegateCommand _submitClickedCommand;
        async public void SubmitButtonClicked(object obj)
        {
            if (AmountColor.Equals("Salmon") || CategoryColor.Equals("Salmon")
                || AvailCategories.Count == 0 || SelectedCategoryIndex == -1)
            {
                await parentPage.DisplayAlert("Error!", "Some of you input is invalid. Please" +
                    " be sure that you have selected a record type, valid amount and valid category.",
                    "Ok");
            }
            else
            {
                var response =
                    await parentPage.DisplayAlert("Proceed?", "Are you sure you want to submit the changes" +
                    " that you have made to the record?", "Yes, I'm sure", "No, don't");
                if (response)
                {
                    record.RecordType = ExpenseChecked ? "expense" : "income";
                    record.Amount = Double.Parse(Amount);
                    record.Date = Date;
                    record.Category = AvailCategories[SelectedCategoryIndex];
                    parentPage.parentPage.vm.Refresh();
                    Application.Current.MainPage = parentPage.parentPage;
                }
            }
        }
        #endregion

        public SelectRecordPageViewModel(SelectRecordPage parent, Record record)
        {
            parentPage = parent;
            this.record = record;
            Amount = this.record.Amount.ToString();
            AmountColor = "Wheat";
            CategoryColor = "Wheat";
            Date = this.record.Date;
            if (this.record.RecordType.Equals("expense"))
            {
                ExpenseChecked = true;
            }
            else
            {
                IncomeChecked = true;
            }
            SubmitEnabled = false;
        }

        private void GetAvailableCategories(bool eChecked, bool iChecked)
        {
            if (eChecked)
            {
                AvailCategories = Categories.GetExpenseCategories();
            }
            else if (iChecked)
            {
                AvailCategories = Categories.GetIncomeCategories();
            }
            else
            {
                AvailCategories = new List<string>();
            }

            for (int i = 0; i < AvailCategories.Count; i++)
            {
                if (AvailCategories[i].Equals(record.Category))
                {
                    SelectedCategoryIndex = i;
                    break;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
