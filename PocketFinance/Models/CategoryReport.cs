using System;
namespace PocketFinance.Models
{
    public class CategoryReport
    {

        #region Properties
        public string CategoryName { get; set; }
        public double Amount { get; set; }
        public double AverageAmount { get; set; }
        public double Percent { get; set; }
        #endregion

        public CategoryReport(string catName)
        {
            CategoryName = catName;
            Amount = 0.0;
            AverageAmount = 0.0;
            Percent = 0.0;
        }
    }
}
