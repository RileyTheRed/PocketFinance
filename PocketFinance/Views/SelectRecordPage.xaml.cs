using System;
using System.Collections.Generic;
using PocketFinance.Models;
using PocketFinance.ViewModels;
using Xamarin.Forms;

namespace PocketFinance.Views
{
    public partial class SelectRecordPage : ContentPage
    {

        public SearchRecordsPage parentPage;

        public SelectRecordPage(SearchRecordsPage parent, Record record, RecordBook book)
        {
            InitializeComponent();
            parentPage = parent;
            BindingContext = new SelectRecordPageViewModel(this, record, book);
        }
    }
}
