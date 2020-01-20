using System;
using System.Collections.Generic;
using System.Linq;
using PocketFinance.Models;

namespace PocketFinance.Utilities
{
    public static class ListComparisonFunctions
    {

        public static List<Record> GetMasterListFromExternalAndLocal(List<Record> eList, List<Record> iList)
        {
            if (eList.Count == 0)
            {
                return iList;
            }
            else if (iList.Count == 0)
            {
                return eList;
            }

            List<Record> masterList = new List<Record>();
            List<Record> eExceptList = new List<Record>();
            List<Record> iExceptList = new List<Record>();

            // first get the intersection of the two for the master list
            // since the equals operator is overloaded then the only records
            // that come of this intersection will have the EXACT same values
            // for everything
            masterList = eList.Intersect(iList).ToList();

            eExceptList = eList.Except(iList).ToList();
            iExceptList = iList.Except(eList).ToList();

            // get the record ids of each of the record lists into their own lists
            List<string> externalIds = eExceptList.Select(r => r.RecordID).ToList();
            List<string> internalIds = iExceptList.Select(r => r.RecordID).ToList();
            List<string> idsIntersect = externalIds.Intersect(internalIds).ToList();

            foreach (Record item in iExceptList)
            {
                if (!externalIds.Contains(item.RecordID))
                {
                    masterList.Add(item);
                }
            }
            foreach (Record item in eExceptList)
            {
                if (!internalIds.Contains(item.RecordID))
                {
                    masterList.Add(item);
                }
            }

            // for each of the records in the externalExcept, look for a matching
            // record in the internalExcept and figure out which one has been modified
            // most recently, choosing that one
            foreach (Record item in eExceptList)
            {
                foreach (Record item1 in iExceptList)
                {
                    if (item.RecordID.Equals(item1.RecordID))
                    {
                        if (item.LastModified > item1.LastModified)
                        {
                            masterList.Add(item);
                        }
                        else
                        {
                            masterList.Add(item1);
                        }
                        iExceptList.Remove(item1);
                        break;
                    }
                }
            }

            return masterList;
        }

    }
}
