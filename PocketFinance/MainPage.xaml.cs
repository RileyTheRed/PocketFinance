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

namespace PocketFinance
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
            Application.Current.MainPage = new NewRecordPage(this, book);
        }

        void btnClicked_NewCategory(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NewCategoryPage(this, book);
        }

        void btnClicked_ViewRecords(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SearchRecordsPage(this, book);
        }

        void btnClicked_ViewCharts(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ViewRecordChartsPage(book, this);
        }
    }
}
