using System;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using PocketFinance.Models;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace PocketFinance.Utilities
{
    public class FirebaseDatabase
    {
        FirebaseClient _firebase;

        public FirebaseDatabase()
        {
            _firebase = new FirebaseClient("https://pocketfinance-66f0f.firebaseio.com/");
        }

        public async Task InsertNewRecords(Record record)
        {
            await _firebase
                .Child("Record")
                .PostAsync(record);
        }

        public async Task UpdateSelectedRecord(Record record)
        {
            var toUpdateRecord = (await _firebase.Child("Record").OnceAsync<Record>())
                .Where(r => r.Object.RecordID.Equals(record.RecordID)).FirstOrDefault();

            await _firebase
                .Child("Record")
                .Child(toUpdateRecord.Key)
                .PutAsync(record);
        }

        public async Task<List<Record>> GetAllRecords()
        {
            var temp = await _firebase
              .Child("Record")
              .OnceAsync<Record>();

            return temp.Select(item => new Record
              {
                  RecordID = item.Object.RecordID,
                  Amount = item.Object.Amount,
                  Date = item.Object.Date,
                  RecordType = item.Object.RecordType,
                  Category = item.Object.Category,
                  Description = item.Object.Description,
                  IsDeleted = item.Object.IsDeleted,
                  IsNew = item.Object.IsNew,
                  IsModified = item.Object.IsModified,
                  LastModified = item.Object.LastModified
              }).ToList();
        }
    }
}
