using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PocketFinance.Models;
using PocketFinance.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocketFinance
{
    public partial class App : Application
    {

        static RecordDatabase database;
        RecordBook recordBook;

        public static RecordDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new RecordDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Records.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            recordBook = new RecordBook();
            MainPage = new MainPage(recordBook);
        }

        async protected override void OnStart()
        {
            // Handle when your app starts
            recordBook.RecordList = await Database.GetNotesAsync();
            List<string> customExpenseTypes = new List<string>();
            List<string> customIncomeTypes = new List<string>();
            List<string> allCoreCategories = Categories.GetExpenseCategories().Union(Categories.GetIncomeCategories()).ToList();
            foreach (Record item in recordBook.RecordList)
            {
                if (!allCoreCategories.Contains(item.Category))
                {
                    if (item.RecordType.Equals("expense"))
                        customExpenseTypes.Add(item.Category);
                    else
                        customIncomeTypes.Add(item.Category);
                }
            }
            foreach (string item in customIncomeTypes)
            {
                recordBook.CustomCategories.Add(new CustomCategory(item, "income"));
            }
            foreach (string item in customExpenseTypes)
            {
                recordBook.CustomCategories.Add(new CustomCategory(item, "expense"));
            }
        }

        async protected override void OnSleep()
        {
            //Console.WriteLine("OnSleep()");
            foreach (Record item in recordBook.RecordList)
            {
                if (item.IsNew || item.IsModified)
                {
                    await Database.SaveNoteAsync(item);
                    item.IsNew = false;
                    item.IsModified = false;
                }
            }
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
