﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PocketFinance.ViewModels;
using Xamarin.Forms;

namespace PocketFinance
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        void btnClicked_NewExpense(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NewExpensePage(this);
        }
    }
}
