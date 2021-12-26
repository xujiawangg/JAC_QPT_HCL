using HfutIe.Config;
using HfutIe.DataAccess.Common;
using HfutIE.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HfutIe.DataAccess.DbProvider
{
    class DbHelperFactory
    {
        //为防止多线程同步运行执行相同的代码，使用lock关键字
        private static readonly object _locker = new object();
        private static Dictionary<string, DbHelper> _dics = new Dictionary<string, DbHelper>();//数据字典（键值对）
        public static DbHelper GetDbHelper(string connName)
        {
            DbHelper dbhelper = null;
            string connectionString = ConfigurationManager.ConnectionStrings[connName].ConnectionString;
            if (!_dics.TryGetValue(connectionString, out dbhelper))//为避免加锁时耗时，在加锁前先判断数据字典是否有值
            {
                lock (_locker)//加锁后的代码同一时刻只允许一个线程执行
                {
                    if (!_dics.TryGetValue(connectionString, out dbhelper))//判断
                    {
                        dbhelper = new DbHelper(connName);
                        _dics.Add(connectionString, dbhelper);
                    }
                }
            }
            return dbhelper;
        }
        public static DbHelper GetDbHelper(string connString,string dbType)
        {
            DbHelper dbhelper = null;
            if (!_dics.TryGetValue(connString, out dbhelper))//为避免加锁时耗时，在加锁前先判断数据字典是否有值
            {
                lock (_locker)//加锁后的代码同一时刻只允许一个线程执行
                {
                    if (!_dics.TryGetValue(connString, out dbhelper))//判断
                    {
                        dbhelper = new DbHelper(connString, dbType);
                        _dics.Add(connString, dbhelper);
                    }
                }
            }
            return dbhelper;
        }
    }
}
