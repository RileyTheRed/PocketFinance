using System;
using System.Collections.Generic;
using PocketFinance.Models;
using PocketFinance.ViewModels;
using Xamarin.Forms;

namespace PocketFinance
{
    public partial class NewExpensePage : ContentPage
    {

        public ContentPage parentPage;
        NewExpensePageViewModel viewModel;

        public NewExpensePage(ContentPage parent, RecordBook book)
        {
            InitializeComponent();
            parentPage = parent;
            viewModel = new NewExpensePageViewModel(this, book);
            BindingContext = viewModel;
        }

        void btnClicked_ReturnButton(object sender, EventArgs e)
        {
            Application.Current.MainPage = parentPage;
        }
    }
}
