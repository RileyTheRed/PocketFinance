using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using PocketFinance.Utilities;

namespace PocketFinance.Models
{
    [XmlRoot(ElementName ="Record")]
    public class Record
    {

        #region Properties
        [XmlIgnore]
        private Random Random = new System.Random();

        [XmlAttribute(DataType = "string")]
        public string RecordID { get; set; }

        [XmlAttribute(DataType = "double")]
        public double Amount { get; set; }

        [XmlAttribute(DataType ="date")]
        public DateTime Date { get; set; }

        [XmlAttribute(DataType ="string")]
        public string RecordType { get; set; } // income vs expense

        [XmlAttribute(DataType ="string")]
        public string Category { get; set; }

        [XmlAttribute(DataType ="string")]
        public string Description { get; set; }

        [XmlAttribute(DataType ="boolean")]
        public bool IsDeleted { get; set; }
        #endregion

        #region Constructors
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
        }
        #endregion

    }
}
