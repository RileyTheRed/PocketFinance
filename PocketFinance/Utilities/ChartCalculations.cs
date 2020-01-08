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

        public static Entry[] GetBarChart(string year, string month, List<Record> records)
        {

            int y = int.Parse(year);
            int m;

            switch (month)
            {
                case "January":
                    m = 1;
                    break;
                case "February":
                    m = 2;
                    break;
                case "March":
                    m = 3;
                    break;
                case "April":
                    m = 4;
                    break;
                case "May":
                    m = 5;
                    break;
                case "June":
                    m = 6;
                    break;
                case "July":
                    m = 7;
                    break;
                case "August":
                    m = 8;
                    break;
                case "September":
                    m = 9;
                    break;
                case "October":
                    m = 10;
                    break;
                case "November":
                    m = 11;
                    break;
                case "December":
                    m = 12;
                    break;
                default: // will never happen
                    m = 1;
                    break;
            }

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
            categoryTotals.Add("Total", total);

            // now create a list of entry values, and keep an inrement variable too
            int i = 0;
            List<Entry> returns = new List<Entry>();
            foreach (KeyValuePair<string, double> pair in categoryTotals)
            {
                returns.Add(new Entry((float)pair.Value)
                {
                    Label = pair.Key,
                    Color = SKColor.Parse(colors[i++])
                });
            }
            //returns.Add(new Entry((float)total)
            //{
            //    Label = "Total"
            //})

            return returns.ToArray();

        }

    }
}
