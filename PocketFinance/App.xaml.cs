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
        static FirebaseDatabase firebaseDatabase;

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

        public static FirebaseDatabase FirebaseDatabase
        {
            get
            {
                if (firebaseDatabase == null)
                {
                    firebaseDatabase = new FirebaseDatabase();
                }
                return firebaseDatabase;
            }
        }

        public App()
        {
            InitializeComponent();
            recordBook = new RecordBook();
            //MainPage = new MainPage(recordBook);
            MainPage = new MasterDetail(recordBook);
        }

        async protected override void OnStart()
        {
            //List<Record> externalRecords = await FirebaseDatabase.GetAllRecords();
            List <Record> internalRecords = await Database.GetNotesAsync();

            //List<Record> masterList = ListComparisonFunctions.GetMasterListFromExternalAndLocal(externalRecords, internalRecords);
            //recordBook.RecordList = masterList;
            recordBook.RecordList = internalRecords;

            // Handle when your app starts
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
            GetRecordsUpdatedOnSleep();
        }

        protected override async void OnResume()
        {
            // Handle when your app resumes
            //List<Record> tempAllExternal = await FirebaseDatabase.GetAllRecords();

            foreach (Record item in recordBook.RecordList)
            {
                if (item.IsNew)
                {
                    await Database.SaveNoteAsync(item);
                    item.IsNew = false;
                    item.IsModified = false;
                    //await FirebaseDatabase.InsertNewRecords(item);
                }
                else if (item.IsModified)
                {
                    item.IsModified = false;
                    await Database.SaveNoteAsync(item);

                    //foreach (Record item1 in tempAllExternal)
                    //{
                    //    if (item.RecordID.Equals(item1.RecordID))
                    //    {
                    //        if (item.LastModified > item1.LastModified)
                    //        {
                    //            await FirebaseDatabase.UpdateSelectedRecord(item);
                    //            break;
                    //        }
                    //    }
                    //}
                }
            }

            //List<Record> externalRecords = await FirebaseDatabase.GetAllRecords();
            List<Record> internalRecords = await Database.GetNotesAsync();

            //List<Record> masterList = ListComparisonFunctions.GetMasterListFromExternalAndLocal(externalRecords, internalRecords);
            //recordBook.RecordList = masterList;
            recordBook.RecordList = internalRecords;

            // Handle when your app starts
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

        //async protected void GetRecordsOnStart()
        //{
        //    List<Record> externalRecords = await FirebaseDatabase.GetAllRecords();
        //    List<Record> internalRecords = await Database.GetNotesAsync();
        //
        //    List<Record> masterList = ListComparisonFunctions.GetMasterListFromExternalAndLocal(externalRecords, internalRecords);
        //    recordBook.RecordList = masterList;
        //
        //    // Handle when your app starts
        //    //recordBook.RecordList = await Database.GetNotesAsync();
        //    List<string> customExpenseTypes = new List<string>();
        //    List<string> customIncomeTypes = new List<string>();
        //    List<string> allCoreCategories = Categories.GetExpenseCategories().Union(Categories.GetIncomeCategories()).ToList();
        //    foreach (Record item in recordBook.RecordList)
        //    {
        //        if (!allCoreCategories.Contains(item.Category))
        //        {
        //            if (item.RecordType.Equals("expense"))
        //                customExpenseTypes.Add(item.Category);
        //            else
        //                customIncomeTypes.Add(item.Category);
        //        }
        //    }
        //    foreach (string item in customIncomeTypes)
        //    {
        //        recordBook.CustomCategories.Add(new CustomCategory(item, "income"));
        //    }
        //    foreach (string item in customExpenseTypes)
        //    {
        //        recordBook.CustomCategories.Add(new CustomCategory(item, "expense"));
        //    }
        //}

        async protected void GetRecordsUpdatedOnSleep()
        {
            //List<Record> tempAllExternal = await FirebaseDatabase.GetAllRecords();
        
            foreach (Record item in recordBook.RecordList)
            {
                if (item.IsNew)
                {
                    await Database.SaveNoteAsync(item);
                    item.IsNew = false;
                    item.IsModified = false;
                    //await FirebaseDatabase.InsertNewRecords(item);
                }
                else if (item.IsModified)
                {
                    item.IsModified = false;
                    await Database.SaveNoteAsync(item);
        
                    //foreach (Record item1 in tempAllExternal)
                    //{
                    //    if (item.RecordID.Equals(item1.RecordID))
                    //    {
                    //        if (item.LastModified > item1.LastModified)
                    //        {
                    //            await FirebaseDatabase.UpdateSelectedRecord(item);
                    //            break;
                    //        }
                    //    }
                    //}
        
                }
            }
        
            //GetRecordsOnStart();
        }
    }
}
