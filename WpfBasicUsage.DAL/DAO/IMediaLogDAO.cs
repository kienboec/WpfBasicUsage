using System.Collections.Generic;
using WpfBasicUsage.Models;

namespace WpfBasicUsage.DAL.DAO {
    public interface IMediaLogDAO {
        MediaLog FindById(int logId);
        MediaLog AddNewItemLog(string logText, MediaItem item);
        IEnumerable<MediaLog> GetLogsForItem(MediaItem item);
    }
}
