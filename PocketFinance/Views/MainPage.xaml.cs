using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketFinance.Models;
using PocketFinance.Utilities;
using PocketFinance.ViewModels;
using Xamarin.Forms;

namespace PocketFinance.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        RecordBook book;

        public MainPage(RecordBook recordBook)
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            book = recordBook;
        }

        void btnClicked_NewRecord(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new NewRecordPage(this, book);
            //(NavigationPage)Application.Current.MainPage
            //NavigationPage.
            Navigation.PushAsync(new NewRecordPage(this, book)
            { Title = "New Record"} );
        }

        void btnClicked_NewCategory(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new NewCategoryPage(this, book);
            Navigation.PushAsync(new NewCategoryPage(this, book)
            { Title = "New Category" });
        }

        void btnClicked_ViewRecords(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new SearchRecordsPage(this, book);
            Navigation.PushAsync(new SearchRecordsPage(this, book)
            { Title = "Search Records" });
        }

        void btnClicked_ViewCharts(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new ViewRecordChartsPage(book, this);
            Navigation.PushAsync(new ViewRecordChartsPage(book, this)
            { Title = "View Charts" });
        }
    }
}
