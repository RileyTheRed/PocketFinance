using System;
using System.Collections.Generic;
using Microcharts;
using PocketFinance.Models;
using PocketFinance.ViewModels;
using Xamarin.Forms;

namespace PocketFinance.Views
{
    public partial class ViewRecordChartsPage : ContentPage
    {

        //RecordBook book;
        //ViewRecordChartsPageViewModel viewModel;
        public MainPage parentPage;

        public ViewRecordChartsPage(RecordBook book, MainPage parent)
        {
            InitializeComponent();
            parentPage = parent;
            BindingContext = new ViewRecordChartsPageViewModel(book, this);
        }
    }
}
