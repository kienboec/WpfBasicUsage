using System.Collections.Generic;
using WpfAppBasicUsage.Models;

namespace WpfBasicUsage.DAL {
    interface DataAccess {
        public List<MediaItem> GetItems();
        public void AddLogToItem(MediaItem item, MediaLog logs);
    }
}
