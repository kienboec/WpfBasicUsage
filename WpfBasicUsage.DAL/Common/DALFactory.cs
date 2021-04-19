using System;
using System.Configuration;
using System.Reflection;
using WpfBasicUsage.DAL.DAO;

namespace WpfBasicUsage.DAL.Common {
    public class DALFactory {
        private static bool useFileSystem = false;
        private static string assemblyName;
        private static Assembly dalAssembly;
        private static IDatabase database;
        private static IFileAccess fileAccess;

        // load DAL assembly
        static DALFactory() {
            useFileSystem = bool.Parse(ConfigurationManager.AppSettings["useFileSystem"]);

            assemblyName = ConfigurationManager.AppSettings["DALSqlAssembly"];
            if (useFileSystem) {
                assemblyName = ConfigurationManager.AppSettings["DALFileAssembly"];
            }

            dalAssembly = Assembly.Load(assemblyName);
        }

        // create database object with connection string from config
        public static IDatabase GetDatabase() {
            if (database == null) {
                database = CreateDatabase();
            }
            return database;
        }

        private static IDatabase CreateDatabase() {
            string connectionString = ConfigurationManager.ConnectionStrings["PostgresSQLConnectionString"].ConnectionString;
            return CreateDatabase(connectionString);
        }

        // create database object with specific connection string
        private static IDatabase CreateDatabase(string connectionString) {
            string databaseClassName = assemblyName + ".Database";
            Type dbClass = dalAssembly.GetType(databaseClassName);

            return Activator.CreateInstance(dbClass,
              new object[] { connectionString }) as IDatabase;
        }

        public static IFileAccess GetFileAccess() {
            if (fileAccess == null) {
                fileAccess = CreateFileAccess();
            }
            return fileAccess;
        }

        private static IFileAccess CreateFileAccess() {
            string startFolder = ConfigurationManager.ConnectionStrings["StartFolderFilePath"].ConnectionString;
            return CreateFileAccess(startFolder);
        }

        private static IFileAccess CreateFileAccess(string startFolder) {
            string databaseClassName = assemblyName + ".FileAccess";
            Type dbClass = dalAssembly.GetType(databaseClassName);

            return Activator.CreateInstance(dbClass,
              new object[] { startFolder }) as IFileAccess;
        }

        // create media item sql/file dao object
        public static IMediaItemDAO CreateMediaItemDAO() {
            string className = assemblyName + ".MediaItemSqlDAO";
            if (useFileSystem) {
                className = assemblyName + ".MediaItemFileDAO";
            }

            Type zoneType = dalAssembly.GetType(className);
            return Activator.CreateInstance(zoneType) as IMediaItemDAO;
        }

        // create media log sql/file dao object
        public static IMediaLogDAO CreateMediaLogDAO() {
            string className = assemblyName + ".MediaLogSqlDAO";
            if (useFileSystem) {
                className = assemblyName + ".MediaLogFileDAO";
            }

            Type zoneType = dalAssembly.GetType(className);
            return Activator.CreateInstance(zoneType) as IMediaLogDAO;
        }
    }
}
