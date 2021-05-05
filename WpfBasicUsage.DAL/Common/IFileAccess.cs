using System;
using System.Collections.Generic;
using System.IO;
using WpfBasicUsage.Models;

namespace WpfBasicUsage.DAL.Common {
    public interface IFileAccess {
        int CreateNewMediaItemFile(string name, string annotation, string url, DateTime creationTime);
        int CreateNewMediaLogFile(string logText, int mediaItemId);
        IEnumerable<FileInfo> SearchFiles(string searchTerm, MediaTypes searchType);
        IEnumerable<FileInfo> GetAllFiles(MediaTypes searchType);
    }
}
