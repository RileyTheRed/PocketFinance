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
        public SearchRecordsPageViewModel vm;
        RecordBook recordBook;

        public SearchRecordsPage(MainPage parent, RecordBook book)
        {
            InitializeComponent();
            parentPage = parent;
            recordBook = book;
            vm = new SearchRecordsPageViewModel(this, book);
            BindingContext = vm;
        }

        public void ViewSelectedRecord(Record record)
        {
            //Application.Current.MainPage = new SelectRecordPage(this, record, recordBook);
            Navigation.PushAsync(new SelectRecordPage(this, record, recordBook));
        }
    }
}
