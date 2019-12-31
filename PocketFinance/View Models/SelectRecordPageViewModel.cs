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

        private string _category;
        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Category"));
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
        #endregion

        #region Commands
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
            Application.Current.MainPage = parentPage.parentPage;
        }
        #endregion

        public SelectRecordPageViewModel(SelectRecordPage parent, Record record)
        {
            parentPage = parent;
            this.record = record;
            Amount = this.record.Amount.ToString();
            Date = this.record.Date;
            if (this.record.RecordType.Equals("expense"))
                ExpenseChecked = true;
            else
                IncomeChecked = true;
            Category = this.record.Category;
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
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
