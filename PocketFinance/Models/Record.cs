using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using PocketFinance.Utilities;
using SQLite;

namespace PocketFinance.Models
{
    public class Record
    {

        #region Properties
        [Ignore]
        private Random Random
        {
            get { return new System.Random(); }
        }

        [PrimaryKey]
        public string RecordID { get; set; }

        public double Amount { get; set; }

        public DateTime Date { get; set; }

        public string RecordType { get; set; } // income vs expense

        public string Category { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        [Ignore]
        public bool IsNew { get; set; }

        [Ignore]
        public bool IsModified { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Record()
        {
            IsNew = false;
            IsModified = false;
        }

        /// <summary>
        /// Default constructor, for example, when reading in record information from a file.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <param name="date"></param>
        /// <param name="recordType"></param>
        /// <param name="category"></param>
        /// <param name="desc"></param>
        /// <param name="isDeleted"></param>
        public Record(string id, double amount, DateTime date, string recordType, string category, string desc, bool isDeleted)
        {
            RecordID = id;
            Amount = amount;
            Date = date;
            RecordType = recordType;
            Category = category;
            Description = desc;
            IsDeleted = isDeleted;
            IsNew = false;
            IsModified = false;
        }

        /// <summary>
        /// New Object constructor, so the record id needs to be computed.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="date"></param>
        /// <param name="recordType"></param>
        /// <param name="category"></param>
        /// <param name="desc"></param>
        /// <param name="isDeleted"></param>
        public Record(double amount, DateTime date, string recordType, string category, string desc, bool isDeleted)
        {
            RecordID = Security.getHashSha256(amount.ToString() + date.ToString() + category + desc + Random.Next());
            Amount = amount;
            Date = date;
            RecordType = recordType;
            Category = category;
            Description = desc;
            IsDeleted = isDeleted;
            IsNew = true;
            IsModified = false;
        }
        #endregion

    }
}
