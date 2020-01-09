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
        //RecordDatabase database;

        public MainPage(RecordBook recordBook)
        {
            InitializeComponent();
            //database = db;
            BindingContext = new MainPageViewModel();
            book = recordBook;
            //book.RecordList = await database.GetNotesAsync();
        }

        //async void getDatabase()
        //{
        //    book.RecordList = await database.GetNotesAsync();
        //}

        void btnClicked_NewRecord(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NewRecordPage(this, book);
        }

        void btnClicked_NewCategory(object sender, EventArgs e)
        {
            //Application.Current.MainPage = new NewRecordPage(this, book);
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
