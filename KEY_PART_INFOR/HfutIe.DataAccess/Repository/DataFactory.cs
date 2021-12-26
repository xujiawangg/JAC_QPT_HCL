using HfutIe.DataAccess;
using HfutIe.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HfutIE.Repository
{
    /// <summary>
    /// 操作数据库工厂
    /// </summary>
    public class DataFactory
    {
        /// <summary>
        /// 当前数据库类型
        /// </summary>
        private static string _DbType = ConfigHelper.AppSettings("ComponentDbType");

        /// <summary>
        /// 获取指定的数据库连接
        /// </summary>
        /// <param name="connName">数据库连接名称</param>
        /// <returns></returns>
        public static IDatabase Database(string connName)
        {
            return new Database(connName);
        }
        /// <summary>
        /// 获取指定的数据库连接
        /// </summary>
        /// <param name="connName">数据库连接名称</param>
        /// <returns></returns>
        public static IDatabase Database(string connString,string dbType)
        {
            return new Database(connString, dbType);
        }
        /// <summary>
        /// 获取指定的数据库连接
        /// 优先级：（连接语句 + 类型）> 名称 > 类型
        /// </summary>
        /// <returns></returns>
        public static IDatabase Database(string dbType = null, string name = null, string dbConnectionString = null)
        {
            ///1.首先判断连接语句是否为空，若连接语句不为空，获取本次连接数据库的类型，若本次连接数据库类型为空，则获取设置的默认连接类型作为本次连接类型
            if (dbConnectionString != null)
            {
                if (dbType == null)
                {
                    dbType = _DbType;
                }
                return Database(dbConnectionString, dbType);
            }
            else
            {
                if (name == null)
                {
                    if (dbType == null)
                    {
                        dbType = _DbType;
                    }
                    switch (dbType)
                    {
                        case "SqlServer":
                            return Database("HfutIEFramework_SqlServer");
                        case "MySql":
                            return Database("HfutIEFramework_MySql");
                        case "Oracle":
                            return Database("HfutIEFramework_Oracle");
                        case "SQLite":
                            return Database("HfutIEFramework_SQLite");
                        default:
                            return null;
                    }
                }
                else
                {
                    return Database("HfutIEFramework_" + name);
                }
            }
        }
    }
}
