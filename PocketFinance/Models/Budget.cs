using System;
using System.Collections.Generic;

namespace PocketFinance.Models
{
    public class Budget
    {

        #region Properties
        public string BudgetName { get; set; }
        public List<BudgetComponent> BudgetComponents { get; set; }

        #endregion

        /// <summary>
        /// Default new budget constructor
        /// </summary>
        /// <param name="n">Budget Name</param>
        public Budget(string n)
        {
            BudgetName = n;
            BudgetComponents = new List<BudgetComponent>();
        }

        /// <summary>
        /// Constructor for existing budgets that have been saved
        /// </summary>
        /// <param name="n">Budget Names</param>
        /// <param name="c">Budget Components</param>
        public Budget(string n, List<BudgetComponent> c)
        {
            BudgetName = n;
            BudgetComponents = c;
        }
    }
}
