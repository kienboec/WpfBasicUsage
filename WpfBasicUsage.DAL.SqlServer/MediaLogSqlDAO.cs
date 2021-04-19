using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using WpfBasicUsage.DAL.Common;
using WpfBasicUsage.DAL.DAO;
using WpfBasicUsage.Models;

namespace WpfBasicUsage.DAL.SqlServer {
    public class MediaLogSqlDAO : IMediaLogDAO {

        private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"MediaLogs\" WHERE \"Id\"=@Id";
        private const string SQL_FIND_BY_MEDIA_ITEM = "SELECT * FROM public.\"MediaLogs\" WHERE \"MediaItemId\"=@MediaItemId";

        private const string SQL_INSERT_NEW_ITEM = "INSERT INTO public.\"MediaLogs\" (\"LogText\", \"MediaItemId\") VALUES (@LogText, @MediaItemId);";

        private IDatabase database;
        private IMediaItemDAO mediaItemDAO;

        public MediaLogSqlDAO() {
            this.database = DALFactory.GetDatabase();
            this.mediaItemDAO = DALFactory.CreateMediaItemDAO();
        }

        public MediaLog FindById(int logId) {
            DbCommand command = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(command, "@Id", DbType.Int32, logId);

            IEnumerable<MediaLog> mediaLogList = QueryMediaLogsFromDb(command);
            return mediaLogList.FirstOrDefault();
        }

        public MediaLog AddNewItemLog(MediaLog log) {
            return AddNewItemLog(log.LogText, log.LogMediaItem);
        }

        public MediaLog AddNewItemLog(string logText, MediaItem item) {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT_NEW_ITEM);
            database.DefineParameter(insertCommand, "@LogText", DbType.String, logText);
            database.DefineParameter(insertCommand, "@MediaItemId", DbType.Int32, item.Id);

            return FindById(database.ExecuteScalar(insertCommand));
        }

        public IEnumerable<MediaLog> GetLogsForItem(MediaItem item) {
            DbCommand command = database.CreateCommand(SQL_FIND_BY_MEDIA_ITEM);
            database.DefineParameter(command, "@MediaItemId", DbType.Int32, item.Id);

            return QueryMediaLogsFromDb(command);
        }

        private IEnumerable<MediaLog> QueryMediaLogsFromDb(DbCommand command) {
            List<MediaLog> mediaLogList = new List<MediaLog>();

            using (IDataReader reader = database.ExecuteReader(command)) {
                while (reader.Read()) {
                    mediaLogList.Add(new MediaLog(
                       (int)reader["Id"],
                       (string)reader["LogText"],
                       mediaItemDAO.FindById((int)reader["MediaItemId"])
                   ));
                }
            }

            return mediaLogList;
        }
    }
}
