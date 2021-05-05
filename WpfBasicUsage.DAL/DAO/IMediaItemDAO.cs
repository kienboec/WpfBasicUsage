using System;
using System.Collections.Generic;
using WpfBasicUsage.Models;

namespace WpfBasicUsage.DAL.DAO {
    public interface IMediaItemDAO {
        MediaItem FindById(int itemId);
        MediaItem AddNewItem(string name, string annotation, string url, DateTime creationTime);
        IEnumerable<MediaItem> GetItems(MediaFolder folder);
    }
}
