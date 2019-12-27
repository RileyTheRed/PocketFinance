using System;
using PocketFinance.Models;

namespace PocketFinance.ViewModels
{
    public class SearchRecordsPageViewModel
    {

        #region Properties
        SearchRecordsPage parentPage;
        public RecordBook recordBook
        {
            get;
            set;
        }
        #endregion

        public SearchRecordsPageViewModel(SearchRecordsPage parent, RecordBook book)
        {
            parentPage = parent;
            recordBook = book;
        }
    }
}
