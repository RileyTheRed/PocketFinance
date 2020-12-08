using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketFinance.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocketFinance
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetail : MasterDetailPage
    {

        RecordBook masterBook;

        public MasterDetail(RecordBook book)
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            masterBook = book;
            Detail = new NavigationPage(new MainPage(masterBook)
            { Title = "Dashboard" });
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterDetailMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType,
                new object[] { masterBook });
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            Title = item.Title;
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}
