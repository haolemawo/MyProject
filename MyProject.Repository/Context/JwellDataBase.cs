using Dapper;
using Dapper.Contrib.Extensions;
using MyProject.Framework.Context;
using System;
using System.Collections.Generic;
using System.Data;

namespace MyProject.Repository.Context
{
    public abstract class MyProjectDataBase<T>: IMyProjectDataBase<T> where T : class
    {
        /// <summary>
        /// 可获取配置文件，可自定义
        /// </summary>
        public abstract string DbName { get; set; }

        /// <summary>
        /// 获取TableName
        /// </summary>
        private string TableName
        {
            get
            {
                //TODO:获取实体类的Table特性
                return typeof(T).Name;
            }
        }


        public IEnumerable<T> Query(string name, object parameters = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            IDbConnection conn = null;
            try
            {
                using (conn = GetConnection())
                {
                    string sql = DataSource.CommandText(name).Trim();
                    return conn.Query<T>(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<T> QueryPaged(string name, int pageIndex, int pageSize, out int count, object parameters = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            pageIndex = Math.Max(pageIndex, 0);
            pageSize = pageSize == 0 ? 20 : pageSize;
            IDbConnection conn = null;
            try
            {
                using (conn = GetConnection())
                {
                    string sql = DataSource.CommandText(name).Trim();

                    count = conn.ExecuteScalar<int>($"SELECT COUNT(1) FROM ({sql}) pagealias", parameters);
                    
                    return conn.Query<T>($"{sql} LIMIT {(pageIndex) * pageSize},{pageSize}", parameters);
                     
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long Add(T entity, bool isTransaction)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            IDbConnection conn = null;
            
            try
            {
                if (!isTransaction)
                {
                    using (conn = GetConnection(false))
                    {
                        return conn.Insert<T>(entity);
                    }
                }
                else
                {
                    IDbTransaction trans = conn.BeginTransaction();
                    conn = trans.Connection;
                    return conn.Insert(entity, trans);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long Add(T entity, out long identityId, bool isTransaction)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            IDbConnection conn = null;
            try
            {
                using (conn = GetConnection(false))
                {
                    return conn.Insert(entity, out identityId, isTransaction);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long AddList(IEnumerable<T> entities, bool isTransaction)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            IDbConnection conn = null;
            try
            {
                if (!isTransaction)
                {
                    using (conn = GetConnection(false))
                    {
                        return conn.Insert<IEnumerable<T>>(entities);
                    }
                }
                else
                {
                    IDbTransaction trans = conn.BeginTransaction();
                    conn = trans.Connection;
                    return conn.Insert<IEnumerable<T>>(entities, trans);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(T entity, bool isTransaction)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            IDbConnection conn = null;
            
            try
            {
                if (!isTransaction)
                {
                    using (conn = GetConnection())
                    {
                        return conn.Update(entity);
                    }
                }
                else
                {
                    IDbTransaction trans = conn.BeginTransaction();
                    conn = trans.Connection;
                    return conn.Update(entity, trans);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取第一行第一列数据
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="isReadDb"></param>
        /// <returns></returns>
        public U ExecuteScalar<U>(string name, object parameters = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            IDbConnection conn = null;
            try
            {
                using (conn = GetConnection())
                {
                    string sql = DataSource.CommandText(name).Trim();
                    return conn.ExecuteScalar<U>(sql,parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 此方法会默认写库
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public int ExecuteWriteSql(string name, object parameters = null, IDbTransaction trans = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            IDbConnection conn = null;
            try
            {
                string sql = DataSource.CommandText(name).Trim();
                if (trans == null)
                {
                    using (conn = GetConnection(false))
                    {
                        return conn.Execute(sql, parameters);
                    }
                }
                else
                {
                    conn = trans.Connection;
                    return conn.Execute(name, parameters, trans);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T QueryFirst(string name, object parameters = null)
        {
            IDbConnection conn = null;
            try
            {
                using (conn = GetConnection())
                {
                    string sql = DataSource.CommandText(name).Trim();
                    return conn.QueryFirst<T>(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T QueryFirstOrDefault(string name, object parameters = null)
        {
            IDbConnection conn = null;
            try
            {
                using (conn = GetConnection())
                {
                    string sql = DataSource.CommandText(name).Trim();
                    return conn.QueryFirstOrDefault<T>(sql, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T GetById(long id)
        {
            IDbConnection conn = null;
            try
            {
                using (conn = GetConnection())
                {
                    return conn.Get<T>(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T GetById(string id)
        {
            IDbConnection conn = null;
            try
            {
                using (conn = GetConnection())
                {
                    return conn.Get<T>(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Delete(T entity)
        {
            try
            {
                using (var conn = GetConnection(false))
                {
                    return conn.Delete(entity);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 获取数据库连接对象
        /// </summary>
        /// <param name="isReadDb">是否是读库，false为写库</param>
        /// <param name="name">配置文件Sql的name</param>
        /// <returns></returns>
        public IDbConnection GetConnection(bool isReadDb = true, string name = "")
        {
            if (string.IsNullOrWhiteSpace(DbName))
            {
                DbName = DataSource.DbName(name);
            }
            return DataSource.GetConnection(DbName, isReadDb);
        }
    }
}
