using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using WpfBasicUsage.DAL.Common;
using WpfBasicUsage.Models;

namespace WpfBasicUsage.DAL.FileServer {
    public class FileAccess : IFileAccess {

        private string filePath;

        public FileAccess(string filePath) {
            this.filePath = filePath;
        }

        private IEnumerable<FileInfo> GetFileInfos(string startFolder, MediaTypes searchType) {
            DirectoryInfo dir = new DirectoryInfo(startFolder);
            return dir.GetFiles("*"+ searchType.ToString() + ".txt", SearchOption.AllDirectories);
        }

        private string GetFullPath(string fileName) {
            return Path.Combine(filePath, Path.GetFileName(fileName));
        }

        public IEnumerable<FileInfo> SearchFiles(string searchTerm, MediaTypes searchType) {

            IEnumerable<FileInfo> fileList = GetFileInfos(filePath, searchType);

            // Search the contents of each file.  
            // A regular expression created with the RegEx class  
            // could be used instead of the Contains method.   
            IEnumerable<FileInfo> queryMatchingFiles =
                from file in fileList
                where file.Extension == ".txt"
                let fileText = GetFileText(file.FullName, searchType)
                where fileText.Contains(searchTerm)
                select file;

            return queryMatchingFiles;
        }

        private string GetFileText(string name, MediaTypes searchType) {
            IEnumerable<FileInfo> fileList = GetFileInfos(filePath, searchType);
            FileInfo foundFile = fileList.Where(file => file.Name.Equals(name))
                .FirstOrDefault();

            if (foundFile != null) {
                using (StreamReader sr = foundFile.OpenText()) {
                    StringBuilder sb = new StringBuilder();
                    while (!sr.EndOfStream) {
                        sb.Append(sr);
                    }
                    return sb.ToString();
                }
            }
            return string.Empty;
        }

        public int CreateNewMediaItemFile(string name, string annotation, string url, DateTime creationTime) {
            // Generate new Id for new file
            int id = Guid.NewGuid().GetHashCode();

            // Create a file to write to
            string fileName = id + "_mediaItem.txt";
            string path = GetFullPath(fileName);
            using (StreamWriter sw = File.CreateText(path)) {
                sw.WriteLine(id);
                sw.WriteLine(name);
                sw.WriteLine(annotation);
                sw.WriteLine(url);
                sw.WriteLine(creationTime);
            }
            return id;
        }

        public int CreateNewMediaLogFile(string logText, int mediaItemId) {
            // Generate new Id for new file
            int id = Guid.NewGuid().GetHashCode();

            // Create a file to write to
            string fileName = id + "_mediaLog.txt";
            string path = GetFullPath(fileName);
            using (StreamWriter sw = File.CreateText(path)) {
                sw.WriteLine(id);
                sw.WriteLine(logText);
                sw.WriteLine(mediaItemId);
            }
            return id;
        }

        public IEnumerable<FileInfo> GetAllFiles(MediaTypes searchType) {
            return GetFileInfos(filePath, searchType);
        }
    }
}
