using System.Collections.Generic;
using System.IO;
using System.Linq;
using WpfBasicUsage.DAL.Common;
using WpfBasicUsage.DAL.DAO;
using WpfBasicUsage.Models;

namespace WpfBasicUsage.DAL.FileServer {
    public class MediaLogFileDAO : IMediaLogDAO {

        private IFileAccess fileAccess;
        private IMediaItemDAO mediaItemDAO;

        public MediaLogFileDAO() {
            this.fileAccess = DALFactory.GetFileAccess();
            this.mediaItemDAO = DALFactory.CreateMediaItemDAO();
        }

        public MediaLog AddNewItemLog(string logText, MediaItem item) {
            int id = fileAccess.CreateNewMediaLogFile(logText, item.Id);
            return new MediaLog(id, logText, item);
        }

        public MediaLog FindById(int logId) {
            IEnumerable<FileInfo> foundFiles = fileAccess.SearchFiles(logId.ToString(), MediaTypes.MediaLog);
            return QueryFromFileSystem(foundFiles).FirstOrDefault();
        }

        public IEnumerable<MediaLog> GetLogsForItem(MediaItem item) {
            IEnumerable<FileInfo> foundFiles = fileAccess.SearchFiles(item.Id.ToString(), MediaTypes.MediaLog);
            return QueryFromFileSystem(foundFiles);
        }

        private IEnumerable<MediaLog> QueryFromFileSystem(IEnumerable<FileInfo> foundFiles) {
            List<MediaLog> foundMediaLogs = new List<MediaLog>();

            foreach (FileInfo file in foundFiles) {
                string[] fileLines = File.ReadLines(file.FullName).ToArray();
                foundMediaLogs.Add(new MediaLog(
                    int.Parse(fileLines[0]),        // id
                    fileLines[1],                   // logText
                    mediaItemDAO.FindById(int.Parse(fileLines[2]))         // mediaItemId
                ));
            }

            return foundMediaLogs;
        }
    }
}
