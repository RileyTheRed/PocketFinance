using System;
using System.Collections.Generic;
using PocketFinance.Models;
using PocketFinance.ViewModels;
using Xamarin.Forms;

namespace PocketFinance
{
    public partial class SearchRecordsPage : ContentPage
    {

        public MainPage parentPage;

        public SearchRecordsPage(MainPage parent, RecordBook book)
        {
            InitializeComponent();
            parentPage = parent;
            BindingContext = new SearchRecordsPageViewModel(this, book);
        }
    }
}
