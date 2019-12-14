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
        #endregion

        public NewExpensePageViewModel(NewExpensePage parent)
        {
            parentPage = parent;
            //Amount = "12.0";
            EntryAmountColor = "Wheat";
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
