using System;
using System.Collections.Generic;
using PocketFinance.Models;
using PocketFinance.ViewModels;
using Xamarin.Forms;

namespace PocketFinance
{
    public partial class NewIncomePage : ContentPage
    {

        public ContentPage parentPage;
        NewIncomePageViewModel viewModel;

        public NewIncomePage(ContentPage parent, RecordBook book)
        {
            InitializeComponent();
            parentPage = parent;
            viewModel = new NewIncomePageViewModel(this, book);
            BindingContext = viewModel;
        }

        void btnClicked_ReturnButton(object sender, EventArgs e)
        {
            Application.Current.MainPage = parentPage;
        }
    }
}
