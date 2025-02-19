﻿using System.Data;
using System.Data.Common;

namespace WpfBasicUsage.DAL.Common {
    public interface IDatabase {
        DbCommand CreateCommand(string genericCommandText);
        int DeclareParameter(DbCommand command, string name, DbType type);
        void DefineParameter(DbCommand command, string name, DbType type, object value);
        void SetParameter(DbCommand command, string name, object value);

        IDataReader ExecuteReader(DbCommand command);
        int ExecuteScalar(DbCommand command);
    }
}
