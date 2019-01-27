using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace MyProject.Framework.Context
{
    internal class ConnectionManager
    {

        private DbConnection dbConnection;

        private string connString = string.Empty;

        internal DbConnection GetConnection(string dbName, bool isReadDb = true)
        {
            if (!string.IsNullOrWhiteSpace(dbName))
            {
                throw new ArgumentException($"数据库名称{dbName}不能为空");
            }
            else
            {
                //TODO：从配置中心获取数据库连接
                //TODO：读写不同
                connString = dbName.Trim();
            }

            try
            {
                dbConnection = new MySql.Data.MySqlClient.MySqlConnection(connString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dbConnection;
          
        }
    }
}
