using System;
using System.IO;
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
        }

        async protected override void OnSleep()
        {
            //Console.WriteLine("OnSleep()");
            foreach (Record item in recordBook.RecordList)
            {
                if (item.IsNew || item.IsModified)
                {
                    var temp = await Database.SaveNoteAsync(item);
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
