using System.Collections.Generic;
using WpfAppBasicUsage.Models;

namespace WpfBasicUsage.DAL {
    class DbConnection : DataAccess {

        private string connectionString;

        public DbConnection() {
            // get connection string from config file
            connectionString = "...";
            // establish connection to DB
        }

        public void AddLogToItem(MediaItem item, MediaLog logs) {
            // add insert/update SQL statement/query here
        }

        public List<MediaItem> GetItems() {
            // add select SQL query here
            return new List<MediaItem>() {
                new MediaItem() { Name = "Item1" },
                new MediaItem() { Name = "Item2" },
                new MediaItem() { Name = "Another" },
                new MediaItem() { Name = "SWEI" },
                new MediaItem() { Name = "FHTW" }
            };
        }
    }
}
