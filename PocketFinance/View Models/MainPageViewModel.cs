using System;
using System.ComponentModel;

namespace PocketFinance.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #region Properties
        public string newExpenseLabel
        {
            get { return "New Expense"; }
        }

        public string newIncomeLabel
        {
            get { return "New Income"; }
        }

        public string viewRecordsLabel
        {
            get { return "View Records"; }
        }
        #endregion

        public MainPageViewModel()
        {
        }
    }
}
