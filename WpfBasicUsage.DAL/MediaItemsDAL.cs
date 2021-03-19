using System.Collections.Generic;
using WpfAppBasicUsage.Models;

namespace WpfBasicUsage.DAL {
    public class MediaItemsDAL {

        private string dbConnection;

        public MediaItemsDAL(string connectionString) {
            // establish connection to DB
            dbConnection = connectionString;
        }

        public IEnumerable<MediaItem> GetItems(MediaFolder folder) {
            // usually querying the disk, or from a DB, or ...
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
