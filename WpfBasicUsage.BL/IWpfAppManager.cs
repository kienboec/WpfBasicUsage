using System;
using System.Collections.Generic;
using WpfBasicUsage.Models;

namespace WpfBasicUsage.BL {
    public interface IWpfAppManager {
        MediaFolder GetMediaFolder(string url);
        IEnumerable<MediaItem> GetItems(MediaFolder folder);
        IEnumerable<MediaItem> SearchForItems(string itemName, MediaFolder folder, bool caseSensitive = false);
        MediaLog CreateItemLog(string logText, MediaItem item);
        MediaItem CreateItem(string name, string annotation, string url, DateTime creationDate);
    }
}
