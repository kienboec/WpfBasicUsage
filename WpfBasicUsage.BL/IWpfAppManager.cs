using System.Collections.Generic;
using WpfAppBasicUsage.Models;

namespace WpfAppBasicUsage.BL {
    public interface IWpfAppManager {
        MediaFolder GetMediaFolder(string url);
        IEnumerable<MediaItem> GetItems(MediaFolder folder);
        IEnumerable<MediaItem> SearchForItems(string itemName, MediaFolder folder, bool caseSensitive = false);
    }
}
