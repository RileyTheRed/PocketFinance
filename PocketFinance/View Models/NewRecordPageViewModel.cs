﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using PocketFinance.Models;
using PocketFinance.Utilities;
using PocketFinance.Views;
using Xamarin.Forms;

namespace PocketFinance.ViewModels
{
    public class NewRecordPageViewModel : INotifyPropertyChanged
    {

        #region Properties
        NewRecordPage parentPage;
        RecordBook recordBook;
        Assembly executingAssembly;

        public bool IsReccuringClicked { get; set; }

        private string _expenseButtonBackgroundColor;
        public string ExpenseButtonBackgroundColor
        {
            get { return _expenseButtonBackgroundColor; }
            set
            {
                _expenseButtonBackgroundColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ExpenseButtonBackgroundColor"));
            }
        }

        private string _incomeButtonBackgroundColor;
        public string IncomeButtonBackgroundColor
        {
            get { return _incomeButtonBackgroundColor; }
            set
            {
                _incomeButtonBackgroundColor = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IncomeButtonBackgroundColor"));
            }
        }

        private ImageSource _checkBoxImage;
        public ImageSource CheckBoxImage
        {
            get { return _checkBoxImage; }
            set
            {
                _checkBoxImage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CheckBoxImage"));
            }
        }

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

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Description"));
            }
        }

        private int _categoryIndex;
        public int CategoryIndex
        {
            get { return _categoryIndex; }
            set
            {
                _categoryIndex = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CategoryIndex"));
            }
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedDate"));
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
                    ExpenseTypes = Categories.GetExpenseCategories().Union(recordBook.CustomCategories.Where(c => c.CategoryType.Equals("expense")).Select(c => c.Category).ToList()).ToList();
                    IncomeChecked = false;
                    ExpenseButtonBackgroundColor = "LightGray";
                }
                else
                {
                    if (IncomeChecked == false)
                    {
                        ExpenseTypes = new List<string>();
                        ExpenseButtonBackgroundColor = "WhiteSmoke";
                    }
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
                    ExpenseTypes = Categories.GetIncomeCategories().Union(recordBook.CustomCategories.Where(c => c.CategoryType.Equals("income")).Select(c => c.Category).ToList()).ToList();
                    ExpenseChecked = false;
                    IncomeButtonBackgroundColor = "LightGray";
                }
                else
                {
                    if (ExpenseChecked == false)
                    {
                        ExpenseTypes = new List<string>();
                        IncomeButtonBackgroundColor = "WhiteSmoke";
                    }
                }
                PropertyChanged(this, new PropertyChangedEventArgs("IncomeChecked"));
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
            set
            {
                _ = value;
                PropertyChanged(this, new PropertyChangedEventArgs("MaxDateValue"));
            }
        }
        public DateTime MinDateValue
        {
            get { return DateTime.MinValue; }
        }

        private List<string> _expenseTypes;
        public List<string> ExpenseTypes
        {
            get { return _expenseTypes; }
            set
            {
                _expenseTypes = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ExpenseTypes"));
             }
        }
        #endregion

        #region Commands
        public ICommand ExpenseButtonCommand
        {
            get
            {
                if (_expenseButtonCommand == null)
                {
                    _expenseButtonCommand = new DelegateCommand(ExpenseButtonCommanded);
                }
                return _expenseButtonCommand;
            }
        }
        DelegateCommand _expenseButtonCommand;
        public void ExpenseButtonCommanded(object obj)
        {
            ExpenseChecked = !ExpenseChecked;
        }

        public ICommand IncomeButtonCommand
        {
            get
            {
                if (_incomeButtonCommand == null)
                {
                    _incomeButtonCommand = new DelegateCommand(IncomeButtonCommanded);
                }
                return _incomeButtonCommand;
            }
        }
        DelegateCommand _incomeButtonCommand;
        public void IncomeButtonCommanded(object obj)
        {
            IncomeChecked = !IncomeChecked;
        }

        public ICommand ReccuringButtonCommand
        {
            get
            {
                if (_recurringButtonCommand == null)
                {
                    _recurringButtonCommand = new DelegateCommand(RecurringButtonClicked);
                }
                return _recurringButtonCommand;
            }
        }
        DelegateCommand _recurringButtonCommand;
        public void RecurringButtonClicked(object obj)
        {
            IsReccuringClicked = !IsReccuringClicked;
            if (IsReccuringClicked)
                CheckBoxImage = ImageSource.FromResource("PocketFinance.Images.checkboxChecked_48px.png", executingAssembly);
            else
                CheckBoxImage = ImageSource.FromResource("PocketFinance.Images.checkboxUnchecked_48px.png", executingAssembly);
        }

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
                _ = Double.Parse(_amount);
                EntryAmountColor = "White";
            }
            catch
            {
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
            if (CategoryIndex == -1)
            {
                PickerCategoryColor = "Salmon";
            }
            else
            {
                PickerCategoryColor = "Wheat";
            }
        }

        public ICommand SubmitRecordCommand
        {
            get
            {
                if (_submitRecordCommand == null)
                {
                    _submitRecordCommand = new DelegateCommand(SubmitClicked);
                }
                return _submitRecordCommand;
            }
        }
        DelegateCommand _submitRecordCommand;
        async public void SubmitClicked(object obj)
        {
            if (CategoryIndex != -1 &&
                !EntryAmountColor.Equals("Salmon") &&
                SelectedDate != MinDateValue)
            {
                string recordType = IncomeChecked ? "income" : "expense";
                recordBook.RecordList.Add(
                    new Record(Double.Parse(Amount), SelectedDate, recordType, ExpenseTypes[CategoryIndex], Description ?? "", false)
                    );
                var response = await parentPage.DisplayAlert("Success!", "Record added! Want to add another?",
                    "Yes", "No, I'm Done");
                if (!response)
                {
                    //Application.Current.MainPage = parentPage.parentPage;
                    await parentPage.Navigation.PopAsync();
                }
                else
                {
                    Amount = "";
                    Description = "";
                    CategoryIndex = -1;
                    SelectedDate = MaxDateValue;

                    EntryAmountColor = "Wheat";
                    PickerCategoryColor = "Wheat";
                    DatePickerColor = "Wheat";
                }
            }
            else
            {
                await parentPage.DisplayAlert(
                    "Error!",
                    "Please check the information input and make sure that a record type has been selected.",
                    "Ok"
                    );
            }
        }
        #endregion

        public NewRecordPageViewModel(NewRecordPage parent, RecordBook book)
        {
            parentPage = parent;
            //EntryAmountColor = "Wheat";
            //PickerCategoryColor = "Wheat";
            //DatePickerColor = "Wheat";
            MaxDateValue = DateTime.Now;
            SelectedDate = DateTime.Now;
            recordBook = book;
            IncomeChecked = false;
            ExpenseChecked = false;
            ExpenseTypes = new List<string>();
            CategoryIndex = -1;
            executingAssembly = Assembly.GetExecutingAssembly();
            CheckBoxImage = ImageSource.FromResource("PocketFinance.Images.checkboxUnchecked_48px.png", executingAssembly);
            IsReccuringClicked = false;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
