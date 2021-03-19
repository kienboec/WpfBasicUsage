using System.Collections.Generic;
using System.Linq;
using WpfAppBasicUsage.Models;
using WpfBasicUsage.DAL;

namespace WpfAppBasicUsage.BL {
    internal class WpfAppManagerImpl : IWpfAppManager {

        public IEnumerable<MediaItem> GetItems(MediaFolder folder) {
            MediaItemsDAL mediaItemDal = new MediaItemsDAL("coolerConnectionString");
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
    }
}
