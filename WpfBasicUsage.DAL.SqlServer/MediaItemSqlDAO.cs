using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using WpfBasicUsage.DAL.Common;
using WpfBasicUsage.DAL.DAO;
using WpfBasicUsage.Models;

namespace WpfBasicUsage.DAL.SqlServer {
    public class MediaItemSqlDAO : IMediaItemDAO {

        private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"MediaItems\" WHERE \"Id\"=@Id;";
        private const string SQL_GET_ALL_ITEMS = "SELECT * FROM public.\"MediaItems\";";
        private const string SQL_INSERT_NEW_ITEM = "INSERT INTO public.\"MediaItems\" (\"Name\", \"Annotation\", \"Url\", \"CreationTime\") VALUES (@Name, @Annotation, @Url, @CreationTime) RETURNING \"Id\";";

        private IDatabase database;

        public MediaItemSqlDAO() {
            this.database = DALFactory.GetDatabase();
        }

        public MediaItemSqlDAO(IDatabase database) {
            this.database = database;
        }

        public MediaItem FindById(int itemId) {
            DbCommand command = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(command, "@Id", DbType.Int32, itemId);

            IEnumerable<MediaItem> mediaItems = QueryMediaItemsFromDb(command);
            return mediaItems.FirstOrDefault();
        }

        public MediaItem AddNewItem(MediaItem item) {
            return AddNewItem(item.Name, item.Annotation, item.Url, item.CreationTime);
        }

        public MediaItem AddNewItem(string name, string annotation, string url, DateTime creationTime) {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT_NEW_ITEM);
            database.DefineParameter(insertCommand, "@Name", DbType.String, name);
            database.DefineParameter(insertCommand, "@Annotation", DbType.String, annotation);
            database.DefineParameter(insertCommand, "@Url", DbType.String, url);
            database.DefineParameter(insertCommand, "@CreationTime", DbType.String, creationTime.ToString());

            return FindById(database.ExecuteScalar(insertCommand));
        }

        public IEnumerable<MediaItem> GetItems(MediaFolder folder) {
            DbCommand command = database.CreateCommand(SQL_GET_ALL_ITEMS);
            return QueryMediaItemsFromDb(command);
        }

        private IEnumerable<MediaItem> QueryMediaItemsFromDb(DbCommand command) {
            List<MediaItem> mediaItemList = new List<MediaItem>();

            using (IDataReader reader = database.ExecuteReader(command)) {
                while (reader.Read()) {
                    mediaItemList.Add(new MediaItem(
                        (int)reader["Id"],
                        (string)reader["Name"],
                        (string)reader["Annotation"],
                        (string)reader["Url"],
                        DateTime.Parse(reader["CreationTime"].ToString())
                    ));
                }
            }

            return mediaItemList;
        }
    }
}
