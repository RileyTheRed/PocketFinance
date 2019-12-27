using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketFinance.Models;
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

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            book = new RecordBook();
        }

        void btnClicked_NewRecord(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NewRecordPage(this, book);
        }
    }
}
