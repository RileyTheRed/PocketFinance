using System;
using System.Collections.Generic;
using PocketFinance.Models;
using PocketFinance.ViewModels;
using Xamarin.Forms;

namespace PocketFinance
{
    public partial class NewCategoryPage : ContentPage
    {
        public MainPage parentPage;

        public NewCategoryPage(MainPage parent, RecordBook book)
        {
            InitializeComponent();
            parentPage = parent;
            BindingContext = new NewCategoryPageViewModel(this, book);
        }
    }
}
