using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using PocketFinance.Utilities;

namespace PocketFinance.ViewModels
{
    public class NewExpensePageViewModel : INotifyPropertyChanged
    {



        #region Properties
        NewExpensePage parentPage;
        private double recordAmount;

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

        private string _entryAmountColor;
        public string EntryAmountColor
        {
            get { return _entryAmountColor; }
            set
            {
                _entryAmountColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EntryAmountColor"));
            }
        }

        private string _pickerCategoryColor;
        public string PickerCategoryColor
        {
            get { return _pickerCategoryColor; }
            set
            {
                _pickerCategoryColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PickerCategoryColor"));
            }
        }

        private string _datePickerColor;
        public string DatePickerColor
        {
            get { return _datePickerColor; }
            set
            {
                _datePickerColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DatePickerColor"));
            }
        }

        public DateTime MaxDateValue
        {
            get { return DateTime.Now.Date; }
        }
        public DateTime MinDateValue
        {
            get { return DateTime.MinValue; }
        }

        public List<string> expenseTypes
        {
            get { return Categories.GetExpenseCategories(); }
        }
        #endregion





        #region Commands
        public ICommand LostFocusAmount
        {
            get
            {
                if (_lostFocusAmount == null)
                {
                    _lostFocusAmount = new DelegateCommand(AmountLostFocus);
                }
                return _lostFocusAmount;
            }
        }
        DelegateCommand _lostFocusAmount;
        public void AmountLostFocus(object obj)
        {
            try
            {
                recordAmount = Double.Parse(_amount);
                EntryAmountColor = "Wheat";
            }
            catch
            {
                parentPage.DisplayAlert("Invalid", "Invalid amout!", "Ok");
                EntryAmountColor = "Salmon";
            }
        }

        public ICommand LostFocusCategory
        {
            get
            {
                if (_lostFocusCategory == null)
                {
                    _lostFocusCategory = new DelegateCommand(CategoryLostFocus);
                }
                return _lostFocusCategory;
            }
        }
        DelegateCommand _lostFocusCategory;
        public void CategoryLostFocus(object obj)
        {
            if (Category.Equals("Select a Category..."))
            {
                parentPage.DisplayAlert("Alert", "Invalid Category!", "Ok");
                PickerCategoryColor = "Salmon";
            }
            else
            {
                PickerCategoryColor = "Wheat";
            }
        }
        #endregion

        public NewExpensePageViewModel(NewExpensePage parent)
        {
            parentPage = parent;
            Category = "Select a Category...";
            EntryAmountColor = "Wheat";
            PickerCategoryColor = "Wheat";
            
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
