using System.Collections.Generic;
using System.Linq;
using WpfBasicUsage.Models;
using WpfBasicUsage.DAL;

namespace WpfBasicUsage.BL {
    internal class WpfAppManagerImpl : IWpfAppManager {

        MediaItemsDAL mediaItemDal = new MediaItemsDAL();

        public IEnumerable<MediaItem> GetItems(MediaFolder folder) {
            return mediaItemDal.GetItems(folder);
        }

        public MediaFolder GetMediaFolder(string url) {
            // usally located somewhere on the disk
            return new MediaFolder();
        }

        public IEnumerable<MediaItem> SearchForItems(string itemName, MediaFolder folder, bool caseSensitive = false) {
            IEnumerable<MediaItem> items = GetItems(folder);

            if (caseSensitive) {
                return items.Where(x => x.Name.Contains(itemName));
            }
            return items.Where(x => x.Name.ToLower().Contains(itemName.ToLower()));
        }

        public void CreateLogs(MediaItem item, MediaLog logs) {
            mediaItemDal.AddLogToTour(item, logs);
        }
    }
}
