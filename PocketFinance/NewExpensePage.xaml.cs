using System;
using System.Collections.Generic;
using PocketFinance.ViewModels;
using Xamarin.Forms;

namespace PocketFinance
{
    public partial class NewExpensePage : ContentPage
    {

        ContentPage parentPage;
        NewExpensePageViewModel viewModel;

        public NewExpensePage(ContentPage parent)
        {
            InitializeComponent();
            parentPage = parent;
            viewModel = new NewExpensePageViewModel(this);
            BindingContext = viewModel;
        }

        void btnClicked_ReturnButton(object sender, EventArgs e)
        {
            Application.Current.MainPage = parentPage;
        }
    }
}
