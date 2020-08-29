using System;
namespace PocketFinance.Models
{
    public class BudgetComponent
    {

        #region Properties
        public string ComponentName { get; set; }
        public float ComponentBudget { get; set; }
        #endregion

        /// <summary>
        /// Basic Component of a budget
        ///
        /// This is to be used as a part of the overall budget object
        /// Each component has a name and a budget amount
        /// </summary>
        /// <param name="n">Component Name</param>
        /// <param name="b">Component Budget</param>
        public BudgetComponent(string n, float b)
        {
            ComponentName = n;
            ComponentBudget = b;
        }
    }
}
