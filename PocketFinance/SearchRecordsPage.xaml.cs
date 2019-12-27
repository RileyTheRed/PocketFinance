using System;
using System.Collections.Generic;
using PocketFinance.Models;
using PocketFinance.ViewModels;
using Xamarin.Forms;

namespace PocketFinance
{
    public partial class SearchRecordsPage : ContentPage
    {

        MainPage parentPage;
        //SearchRecordsPageViewModel viewModel;

        public SearchRecordsPage(MainPage parent, RecordBook book)
        {
            InitializeComponent();
            //viewModel = new SearchRecordsPageViewModel(this, book);
            BindingContext = new SearchRecordsPageViewModel(this, book);
        }
    }
}
