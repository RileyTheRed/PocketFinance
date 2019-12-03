using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PocketFinance
{
    public partial class NewExpensePage : ContentPage
    {

        ContentPage parentPage;

        public NewExpensePage(ContentPage parent)
        {
            InitializeComponent();
            parentPage = parent;
        }

        void btnClicked_ReturnButton(object sender, EventArgs e)
        {
            Application.Current.MainPage = parentPage;
        }
    }
}
