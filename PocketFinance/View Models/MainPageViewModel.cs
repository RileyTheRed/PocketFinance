using System;
using System.ComponentModel;
using PocketFinance.Models;

namespace PocketFinance.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #region Properties
        private RecordBook _recordBook;
        public RecordBook RecordBook
        {
            get { return _recordBook; }
            set
            {
                _recordBook = value;
                PropertyChanged(this, new PropertyChangedEventArgs("RecordBook"));
            }
        }

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

        public MainPageViewModel(/*RecordBook recordBook*/)
        {
            //RecordBook = recordBook;
        }
    }
}
