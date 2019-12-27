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
        #endregion

        public RecordBook()
        {
            RecordList = new List<Record>();
        }
    }
}
