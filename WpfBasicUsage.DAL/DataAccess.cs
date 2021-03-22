using System.Collections.Generic;
using WpfBasicUsage.Models;

namespace WpfBasicUsage.DAL {
    interface DataAccess {
        public List<MediaItem> GetItems();
        public void AddLogToItem(MediaItem item, MediaLog logs);
    }
}
