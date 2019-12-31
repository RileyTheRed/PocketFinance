using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using PocketFinance.Models;
using PocketFinance.Utilities;
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

        private int _selectedCategory;
        public int SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedCategory"));
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
                SetCategories(value, IncomeChecked);
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
                SetCategories(ExpenseChecked, value);
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

        private List<Record> _searchResult;
        public List<Record> SearchResult
        {
            get { return _searchResult; }
            set
            {
                _searchResult = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SearchResult"));
            }
        }

        private Record _selectedListItem;
        public Record SelectedListItem
        {
            get { return _selectedListItem; }
            set
            {
                _selectedListItem = value;
                if (value != null)
                {
                    parentPage.ViewSelectedRecord(value);
                }
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

        public ICommand SearchClickedCommand
        {
            get
            {
                if (_searchClickedCommand == null)
                {
                    _searchClickedCommand = new DelegateCommand(SearchButtonClicked);
                }
                return _searchClickedCommand;
            }
        }
        DelegateCommand _searchClickedCommand;
        public void SearchButtonClicked(object obj)
        {
            SearchResult = SearchResults.GetSearchResults(ExpenseChecked, IncomeChecked, PastMonthChecked,
                PastThreeMonthChecked, PastSixMonthChecked, SelectedCategory, AvailCategories, recordBook.RecordList);
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
            SearchResult = new List<Record>();
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #region Helper
        private void SetCategories(bool eChecked, bool iChecked)
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
                var temp = new List<string>();
                foreach (string item in Categories.GetExpenseCategories())
                {
                    temp.Add(item);
                }
                foreach (string item in Categories.GetIncomeCategories())
                {
                    temp.Add(item);
                }
                AvailCategories = temp;
            }
        }
        #endregion

    }
}
