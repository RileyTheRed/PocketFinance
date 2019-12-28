using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using PocketFinance.Models;
using Xamarin.Forms;

namespace PocketFinance.ViewModels
{
    public class SearchRecordsPageViewModel : INotifyPropertyChanged
    {

        #region Properties
        SearchRecordsPage parentPage;
        public RecordBook recordBook
        {
            get;
            set;
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
                PropertyChanged(this, new PropertyChangedEventArgs("IncomeChecked"));
            }
        }

        private bool _pastMonthChecked;
        public bool PastMonthChecked
        {
            get { return _pastMonthChecked; }
            set
            {
                _pastMonthChecked = value;
                if (value == true)
                {
                    PastThreeMonthChecked = false;
                    PastSixMonthChecked = false;
                }
                PropertyChanged(this, new PropertyChangedEventArgs("PastMonthChecked"));
            }
        }

        private bool _pastThreeMonthChecked;
        public bool PastThreeMonthChecked
        {
            get { return _pastThreeMonthChecked; }
            set
            {
                _pastThreeMonthChecked = value;
                if (value == true)
                {
                    PastMonthChecked = false;
                    PastSixMonthChecked = false;
                }
                PropertyChanged(this, new PropertyChangedEventArgs("PastThreeMonthChecked"));
            }
        }

        private bool _pastSixMonthChecked;
        public bool PastSixMonthChecked
        {
            get { return _pastSixMonthChecked; }
            set
            {
                _pastSixMonthChecked = value;
                if (value == true)
                {
                    PastMonthChecked = false;
                    PastThreeMonthChecked = false;
                }
                PropertyChanged(this, new PropertyChangedEventArgs("PastSixMonthChecked"));
            }
        }

        private List<Record> _searchResults;
        public List<Record> SearchResults
        {
            get { return _searchResults; }
            set
            {
                _searchResults = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SearchResults"));
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

        public SearchRecordsPageViewModel(SearchRecordsPage parent, RecordBook book)
        {
            parentPage = parent;
            recordBook = book;
            ExpenseChecked = false;
            IncomeChecked = false;
            PastMonthChecked = false;
            PastThreeMonthChecked = false;
            PastSixMonthChecked = false;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

    }
}
