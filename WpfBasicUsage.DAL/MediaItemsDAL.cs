using System.Collections.Generic;
using WpfAppBasicUsage.Models;

namespace WpfBasicUsage.DAL {
    public class MediaItemsDAL {

        private DataAccess dataAccess;

        public MediaItemsDAL() {
            // check which datasource to use
            // e.g. use config file for this
            dataAccess = new DbConnection();
            //dataAccess = new FileAccess();
        }

        public IEnumerable<MediaItem> GetItems(MediaFolder folder) {
            // usually querying the disk, or from a DB, or ...
            return dataAccess.GetItems();
        }

        public void AddLogToTour(MediaItem item, MediaLog logs) {
            dataAccess.AddLogToItem(item, logs);
        }
    }
}
