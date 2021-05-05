using System.Collections.Generic;
using System.Linq;
using WpfBasicUsage.Models;
using WpfBasicUsage.DAL.DAO;
using WpfBasicUsage.DAL.Common;
using System;

namespace WpfBasicUsage.BL {
    internal class WpfAppManagerImpl : IWpfAppManager {

        public IEnumerable<MediaItem> GetItems(MediaFolder folder) {
            IMediaItemDAO mediaItemDao = DALFactory.CreateMediaItemDAO();
            return mediaItemDao.GetItems(folder);
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

        public MediaLog CreateItemLog(string logText, MediaItem item) {
            IMediaLogDAO mediaLogDao = DALFactory.CreateMediaLogDAO();
            return mediaLogDao.AddNewItemLog(logText, item);
        }

        public MediaItem CreateItem(string name, string annotation, string url, DateTime creationDate) {
            IMediaItemDAO mediaItemDao = DALFactory.CreateMediaItemDAO();
            return mediaItemDao.AddNewItem(name, annotation, url, creationDate);
        }
    }
}
