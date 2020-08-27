using System;
using System.Collections.Generic;
using System.Linq;
using Microcharts;
using PocketFinance.Models;
using SkiaSharp;

namespace PocketFinance.Utilities
{
    public static class ChartCalculations
    {
        
        static List<string> colors = new List<string>
        {
            "#E6E6FA",
            "#D8BFD8",
            "#DDA0DD",
            "#EE82EE",
            "#DA70D6",
            "#FF00FF",
            "#FF00FF",
            "#BA55D3",
            "#9370DB",
            "#663399",
            "#8A2BE2",
            "#9932CC",
            "#8B008B",
            "#800080",
            "#4B0082",
            "#6A5ACD",
            "#483D8B",
            "#7B68EE"
        };

        static int GetMonth(string month)
        {
            switch (month)
            {
                case "January":
                    return 1;
                case "February":
                    return 2;
                case "March":
                    return 3;
                case "April":
                    return 4;
                case "May":
                    return 5;
                case "June":
                    return 6;
                case "July":
                    return 7;
                case "August":
                    return 8;
                case "September":
                    return 9;
                case "October":
                    return 10;
                case "November":
                    return 11;
                case "December":
                    return 12;
                default:
                    return 1;
            }
        }

        public static ChartEntry[] GetBarChart(string year, string month, List<Record> records)
        {

            int y = int.Parse(year);
            int m = GetMonth(month);

            // get a list of all the expense records for the year and month
            List<Record> tempList1 = records.Where(r =>
                                                   r.Date.Year == y &&
                                                   r.Date.Month == m &&
                                                   r.RecordType.Equals("expense") &&
                                                   !r.IsDeleted)
                                            .ToList();

            // create dictionary and populate the categories as we go!
            // also keep track of the total being logged
            Dictionary<string, double> categoryTotals = new Dictionary<string, double>();
            double total = 0.0;
            foreach (Record item in tempList1)
            {
                if (!categoryTotals.ContainsKey(item.Category))
                {
                    categoryTotals.Add(item.Category, item.Amount);
                }
                else
                {
                    categoryTotals[item.Category] += item.Amount;
                }
                total += item.Amount;
            }

            // now create a list of entry values, and keep an inrement variable too
            int i = 0;
            List<ChartEntry> returns = new List<ChartEntry>();
            foreach (KeyValuePair<string, double> pair in categoryTotals)
            {
                returns.Add(new ChartEntry((float)pair.Value)
                {
                    Label = pair.Key,
                    Color = SKColor.Parse(colors[i++]),
                    ValueLabel = pair.Value.ToString()
                }) ;
            }

            return returns.ToArray();

        }

        public static List<CategoryReport> GetCategoryReports(string year, string month, List<Record> records)
        {

            //List<CategoryReport> returnList = new List<CategoryReport>();
            Dictionary<string, CategoryReport> categoryReports = new Dictionary<string, CategoryReport>();
            int y = int.Parse(year);
            int m = GetMonth(month);

            // get all expense records for this year and month
            List<Record> tempCurrentYearAndMonth = records
                .Where(r => r.Date.Year == y &&
                            r.Date.Month == m &&
                            r.RecordType.Equals("expense") &&
                            !r.IsDeleted)
                .ToList();

            // start by getting each of the categories used this month
            // also hold on to the total in order to add a TOTAL category at the end
            double total = 0.0;
            foreach (Record item in tempCurrentYearAndMonth)
            {
                if (!categoryReports.ContainsKey(item.Category))
                {
                    categoryReports.Add(item.Category, new CategoryReport(item.Category));
                }
                categoryReports[item.Category].Amount += item.Amount;
                total += item.Amount;
            }
            // TODO in category code, make sure that user can not enter TOTAL as a category name
            // TODO in category comparison, implement a ToLower() or ToUpper() check ***
            categoryReports.Add("TOTAL", new CategoryReport("TOTAL"));
            categoryReports["TOTAL"].Amount = total;

            // now get all expense records where the records is NOT from this month AND year
            List<Record> tempNotCurrentMonthOrYear = records
                .Where(r => (r.Date.Year != y ||
                            r.Date.Month != m) &&
                            r.RecordType.Equals("expense") &&
                            !r.IsDeleted)
                .ToList();

            // get the first month and year that record keeping began
            DateTime firstYearAndMonth = DateTime.Now;
            foreach (Record item in records)
            {
                if (!item.IsDeleted && (item.Date < firstYearAndMonth))
                {
                    firstYearAndMonth = item.Date;
                }
            }
            int months = ((DateTime.Now.Year - firstYearAndMonth.Year) * 12)
                + (DateTime.Now.Month - firstYearAndMonth.Month);

            // for each of the categories in the category reports, get the total amount
            // from previous months and calculated the average.
            foreach (KeyValuePair<string, CategoryReport> pair in categoryReports)
            {
                double totalAmount = 0.0;

                if (pair.Key.Equals("TOTAL"))
                {
                    totalAmount += tempNotCurrentMonthOrYear
                    .Select(r => r.Amount)
                    .Sum();
                }
                else
                {
                    totalAmount += tempNotCurrentMonthOrYear
                        .Where(r => r.Category.Equals(pair.Key))
                        .Select(r => r.Amount)
                        .Sum();
                }

                if (months > 0)
                {
                    categoryReports[pair.Key].AverageAmount = totalAmount / months;
                }
                else
                {
                    categoryReports[pair.Key].AverageAmount = categoryReports[pair.Key].Amount;
                }
            }

            return categoryReports.Values.ToList();

        }

        public static double GetTotalMonthlyIncome(string year, string month, List<Record> records)
        {
            int y = int.Parse(year);
            int m = GetMonth(month);

            return records
                .Where(r => r.Date.Year == y &&
                            r.Date.Month == m &&
                            r.RecordType.Equals("income") &&
                            !r.IsDeleted)
                .Select(r => r.Amount)
                .Sum();
        }

    }
}
