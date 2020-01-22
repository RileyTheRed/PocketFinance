using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Microcharts;
using PocketFinance.Models;
using PocketFinance.Utilities;
using Xamarin.Forms;

namespace PocketFinance.ViewModels
{
    public class ViewRecordChartsPageViewModel : INotifyPropertyChanged
    {

        RecordBook recordBook;
        ViewRecordChartsPage parentPage;

        Dictionary<int, string> months = new Dictionary<int, string>
        {
            { 1, "January" },
            { 2, "February" },
            { 3, "March" },
            { 4, "April" },
            { 5, "May" },
            { 6, "June" },
            { 7, "July" },
            { 8, "August" },
            { 9, "September" },
            { 10, "October" },
            { 11, "November" },
            { 12, "December" }
        };

        #region Properties
        private string _year;
        public string Year
        {
            get { return _year; }
            set
            {
                _year = value;
                GetRecordMonths();
                PropertyChanged(this, new PropertyChangedEventArgs("Year"));
            }
        }

        private string _month;
        public string Month
        {
            get { return _month; }
            set
            {
                _month = value;
                if (value != null)
                    ExpenseBarChart = new DonutChart() { Entries = ChartCalculations.GetBarChart(Year, value, recordBook.RecordList) };
                PropertyChanged(this, new PropertyChangedEventArgs("Month"));
            }
        }

        private List<string> _availableYears;
        public List<string> AvailableYears
        {
            get { return _availableYears; }
            set
            {
                _availableYears = value;
                PropertyChanged(this, new PropertyChangedEventArgs("AvailableYears"));
            }
        }

        private List<string> _availableMonths;
        public List<string> AvailableMonths
        {
            get { return _availableMonths; }
            set
            {
                _availableMonths = value;
                PropertyChanged(this, new PropertyChangedEventArgs("AvailableMonths"));
            }
        }

        private DonutChart _barChart;
        public DonutChart ExpenseBarChart
        {
            get { return _barChart; }
            set
            {
                _barChart = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ExpenseBarChart"));
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

        public ViewRecordChartsPageViewModel(RecordBook book, ViewRecordChartsPage parent)
        {
            recordBook = book;
            parentPage = parent;
            GetRecordYears();
        }


        /// <summary>
        /// Determines which years have records associated with them. I dont want
        /// to just make all years available for selection in 'AvailableYears'.
        ///
        /// I only want years that have records to be available.
        /// </summary>
        public void GetRecordYears()
        {
            Dictionary<string, bool> pairs = new Dictionary<string, bool>();
            foreach (Record item in recordBook.RecordList)
            {
                if (!item.IsDeleted)
                {
                    if (!pairs.ContainsKey(item.Date.Year.ToString()))
                    {
                        pairs.Add(item.Date.Year.ToString(), true);
                    }
                }
            }
            List<string> temp = new List<string>();
            foreach (KeyValuePair<string, bool> pair in pairs)
            {
                temp.Add(pair.Key);
            }
            AvailableYears = temp;
        }


        /// <summary>
        /// Determines, for the chosen year, which months of that year have records.
        ///
        /// I only want to display months for the specified year that actually have
        /// records filed.
        /// </summary>
        public void GetRecordMonths()
        {
            Dictionary<int, bool> pairs = new Dictionary<int, bool>();
            foreach(Record item in recordBook.RecordList)
            {
                if (!item.IsDeleted)
                {
                    if (item.Date.Year.ToString().Equals(Year) && !pairs.ContainsKey(item.Date.Month))
                    {
                        pairs.Add(item.Date.Month, true);
                    }
                }
            }
            List<string> temp = new List<string>();
            foreach (KeyValuePair<int, bool> pair in pairs)
            {
                temp.Add(months[pair.Key]);
            }
            AvailableMonths = temp;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
