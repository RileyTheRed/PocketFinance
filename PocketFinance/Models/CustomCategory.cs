using System;
using SQLite;

namespace PocketFinance.Models
{
    public class CustomCategory
    {

        #region Properties
        [PrimaryKey]
        public string Category { get; set; }
        public string CategoryType { get; set; }

        [Ignore]
        public bool IsNew { get; set; }
        #endregion

        /// <summary>
        /// Default empty constructor
        /// </summary>
        public CustomCategory()
        {
            IsNew = false;
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="category"></param>
        /// <param name="categoryType"></param>
        public CustomCategory(string category, string categoryType)
        {
            Category = category;
            CategoryType = categoryType;
            IsNew = true;
        }
    }
}
