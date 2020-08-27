using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using PocketFinance.Models;
using PocketFinance.Utilities;
using Xamarin.Forms;

namespace PocketFinance.ViewModels
{
    public class NewCategoryPageViewModel : INotifyPropertyChanged
    {

        #region Properties
        NewCategoryPage parentPage;
        RecordBook recordBook;

        private bool _expenseChecked;
        public bool ExpenseChecked
        {
            get { return _expenseChecked; }
            set
            {
                _expenseChecked = value;
                if (value)
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
                if (value)
                {
                    ExpenseChecked = false;
                }
                PropertyChanged(this, new PropertyChangedEventArgs("IncomeChecked"));
            }
        }

        private string _customCategory;
        public string CustomCategoryName
        {
            get { return _customCategory; }
            set
            {
                _customCategory = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CustomCategoryName"));
            }
        }
        #endregion

        #region Commands
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
            if ((!ExpenseChecked && !IncomeChecked) || CustomCategoryName == null || CustomCategoryName.Equals(""))
            {
                await parentPage.DisplayAlert("Alert!", "You must select a record type and enter a valid category name!", "Ok");
            }
            else
            {
                bool exists = false;
                foreach (CustomCategory item in recordBook.CustomCategories)
                {
                    if (item.Category.Equals(CustomCategoryName))
                    {
                        exists = true;
                        break;
                    }
                }
                foreach (string item in Categories.GetExpenseCategories().Union(Categories.GetIncomeCategories()))
                {
                    if (item.Equals(CustomCategoryName))
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    if (ExpenseChecked)
                        recordBook.CustomCategories.Add(new CustomCategory(CustomCategoryName, "expense"));
                    else
                        recordBook.CustomCategories.Add(new CustomCategory(CustomCategoryName, "income"));

                    await parentPage.DisplayAlert("Success", "New category added!", "Ok");
                    await parentPage.Navigation.PopAsync();
                }
                else
                {
                    await parentPage.DisplayAlert("Error", "Category already exists!", "Ok");
                }
            }
        }
        #endregion

        public NewCategoryPageViewModel(NewCategoryPage parent, RecordBook book)
        {
            parentPage = parent;
            recordBook = book;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
