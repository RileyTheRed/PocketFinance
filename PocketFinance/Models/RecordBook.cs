using System;
using System.Collections.Generic;

namespace PocketFinance.Models
{
    public class RecordBook
    {

        #region Properties
        public List<Record> RecordList
        {
            get;
            set;
        }

        public List<CustomCategory> CustomCategories
        {
            get;
            set;
        }
        #endregion

        public RecordBook()
        {
            //RecordList = new List<Record>()
            //{
            //    new Record(123.1, DateTime.Now, "expense", "Rent", "", false),
            //    new Record(321.1, DateTime.Now, "expense", "Groceries", "", false),
            //    new Record(123.1, DateTime.Now, "expense", "Restaurants", "", false),
            //    new Record(123.1, DateTime.Now, "expense", "Transportation", "", false),
            //    new Record(1965.84, DateTime.Now, "income", "Wages", "", false),
            //    new Record(1964.85, DateTime.Now, "income", "Wages", "", false),
            //    new Record(123.1, DateTime.Now, "income", "Interest", "", false),
            //    new Record(123.1, DateTime.Now, "income", "Rental Income", "", false)
            //};
            RecordList = new List<Record>();
            CustomCategories = new List<CustomCategory>();
        }
    }
}
