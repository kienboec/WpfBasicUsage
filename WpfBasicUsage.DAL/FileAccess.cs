using System.Collections.Generic;
using WpfBasicUsage.Models;

namespace WpfBasicUsage.DAL {
    class FileAccess : DataAccess {

        private string filePath;

        public FileAccess() {
            // get filepath from config file
            filePath = "...";
        }

        public List<MediaItem> GetItems() {
            // get items from file path
            return new List<MediaItem>() {
                new MediaItem() { Name = "Item1" },
                new MediaItem() { Name = "Item2" },
                new MediaItem() { Name = "Another" },
                new MediaItem() { Name = "SWEI" },
                new MediaItem() { Name = "FHTW" }
            };
        }

        public void AddLogToItem(MediaItem item, MediaLog logs) {
            // Insert/Update logic here
        }
    }
}
