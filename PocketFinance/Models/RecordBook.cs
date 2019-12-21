using System;
using System.Collections.Generic;

namespace PocketFinance.Models
{
    public class RecordBook
    {

        #region Properties
        public List<Record> RecordList;
        #endregion

        public RecordBook()
        {
            RecordList = new List<Record>();
        }
    }
}
