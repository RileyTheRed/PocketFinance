﻿using System;
using System.Collections.Generic;
using System.Linq;
using PocketFinance.Models;

namespace PocketFinance.Utilities
{
    public static class SearchResults
    {

        /// <summary>
        /// Takes in all possible search parameters and returns the list of records
        /// that correspond and align with the selected search parameters.
        /// </summary>
        /// <param name="isExpenseChecked"></param>
        /// <param name="isIncomeChecked"></param>
        /// <param name="isLastMonthChecked"></param>
        /// <param name="isLastThreeMonthChecked"></param>
        /// <param name="isLastSixMonthChecked"></param>
        /// <param name="selectedCategoryIndex"></param>
        /// <param name="availableCategories"></param>
        /// <param name="allRecords"></param>
        /// <returns></returns>
        public static List<Record> GetSearchResults(bool isExpenseChecked,
            bool isIncomeChecked,
            bool isLastMonthChecked,
            bool isLastThreeMonthChecked,
            bool isLastSixMonthChecked,
            int selectedCategoryIndex,
            List<string> availableCategories,
            List<Record> allRecords)
        {

            List<Record> temp = allRecords;

            // start off by looking at whether we are needing expense or income records
            if (isExpenseChecked)
            {
                temp = temp.Where(record => record.RecordType.Equals("expense")).ToList();
            }
            else if (isIncomeChecked)
            {
                temp = temp.Where(record => record.RecordType.Equals("income")).ToList();
            }

            // then look at whether a specific time frame was selected
            if (isLastMonthChecked)
            {
                //temp = temp.Where(record => record.Date.)
            }

            return temp;

        }

    }
}