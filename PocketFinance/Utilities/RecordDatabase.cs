using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PocketFinance.Models;
using SQLite;

namespace PocketFinance.Utilities
{
    public class RecordDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public RecordDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Record>().Wait();
        }

        public Task<List<Record>> GetNotesAsync()
        {
            return _database.Table<Record>().ToListAsync();
        }

        public Task<Record> GetNoteAsync(string id)
        {
            return _database.Table<Record>()
                            .Where(i => i.RecordID.Equals(id))
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Record record)
        {
            if (record.IsNew)
            {
                return _database.InsertAsync(record);
            }
            else
            {
                return _database.UpdateAsync(record);
            }
        }

        public Task<int> DeleteNoteAsync(Record record)
        {
            return _database.DeleteAsync(record);
        }
    }
}
