using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WpfBasicUsage.DAL.Common;
using WpfBasicUsage.DAL.DAO;
using WpfBasicUsage.Models;

namespace WpfBasicUsage.DAL.FileServer {
    public class MediaItemFileDAO : IMediaItemDAO {

        private IFileAccess fileAccess;

        public MediaItemFileDAO() {
            this.fileAccess = DALFactory.GetFileAccess();
        }

        public MediaItemFileDAO(IFileAccess fileAccess) {
            this.fileAccess = fileAccess;
        }

        public MediaItem AddNewItem(string name, string annotation, string url, DateTime creationTime) {
            int id = fileAccess.CreateNewMediaItemFile(name, annotation, url, creationTime);
            return new MediaItem(id, name, annotation, url, creationTime);
        }

        public MediaItem FindById(int itemId) {
            IEnumerable<FileInfo> foundFiles = fileAccess.SearchFiles(itemId.ToString(), MediaTypes.MediaItem);
            return QueryFromFileSystem(foundFiles).FirstOrDefault();
        }

        public IEnumerable<MediaItem> GetItems(MediaFolder folder) {
            IEnumerable<FileInfo> foundFiles = fileAccess.GetAllFiles(MediaTypes.MediaItem);
            return QueryFromFileSystem(foundFiles);
        }

        private IEnumerable<MediaItem> QueryFromFileSystem(IEnumerable<FileInfo> foundFiles) {
            List<MediaItem> foundMediaItems = new List<MediaItem>();

            foreach (FileInfo file in foundFiles) {
                string[] fileLines = File.ReadAllLines(file.FullName);
                foundMediaItems.Add(new MediaItem(
                    int.Parse(fileLines[0]),        // id
                    fileLines[1],                   // name
                    fileLines[2],                   // annotation
                    fileLines[3],                   // url
                    DateTime.Parse(fileLines[4])    // creation date
                ));
            }

            return foundMediaItems;
        }
    }
}
