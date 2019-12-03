using System;
using System.ComponentModel;

namespace PocketFinance.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #region Properties
        public static string newExpenseLabel = "New Expense";
        public static string newIncomeLabel = "New Income";
        #endregion

        public MainPageViewModel()
        {
        }
    }
}
