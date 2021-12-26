using HfutIE.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HfutIE.Repository
{
    /// <summary>
    /// 通用的Repository工厂
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryFactory<T> where T : new()
    {
        /// <summary>
        /// 获取默认的Repository
        /// </summary>
        /// <returns></returns>
        public IRepository<T> Repository()
        {
            return new Repository<T>();
        }
        /// <summary>
        /// 根据数据库类型获取通用的Repository
        /// </summary>
        /// <returns></returns>
        public IRepository<T> Repository(DatabaseType dbType)
        {
            return new Repository<T>(dbType.ToString(), null);
        }
        /// <summary>
        /// 根据数据库名称获取通用的Repository
        /// </summary>
        /// <returns></returns>
        public IRepository<T> Repository(string dbName)
        {
            return new Repository<T>(null, dbName);
        }
        /// <summary>
        /// 根据数据库类型和连接语句获取通用的Repository
        /// </summary>
        /// <returns></returns>
        public IRepository<T> Repository(string dbType,string dbConncetionString)
        {
            return new Repository<T>(dbType, null, dbConncetionString);
        }
    }
}
