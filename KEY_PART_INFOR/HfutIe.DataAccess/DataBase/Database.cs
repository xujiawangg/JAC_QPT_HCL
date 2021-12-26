﻿using HfutIe.DataAccess.DbExpand;
using HfutIe.DataAccess.DbProvider;
using HfutIE.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HfutIe.DataAccess
{
    /// <summary>
    /// 操作数据库基类
    /// </summary>
    public class Database : IDatabase, IDisposable
    {
        #region 构造函数
        public string connString { get; set; }
        private DbHelper dbhelper { get; set; }
        /// <summary>
        /// 通过数据库连接名构造
        /// </summary>
        public Database(string connName)
        {
            dbhelper = DbHelperFactory.GetDbHelper(connName);
        }
        /// <summary>
        /// 通过数据库连接语句和数据库类型构造
        /// </summary>
        public Database(string connString,string dbType)
        {
            dbhelper = DbHelperFactory.GetDbHelper(connString, dbType);
        }
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        private DbConnection dbConnection { get; set; }
        /// <summary>
        /// 事务对象
        /// </summary>
        private DbTransaction isOpenTrans { get; set; }
        /// <summary>
        /// 是否已在事务之中
        /// </summary>
        public bool inTransaction { get; set; }
        /// <summary>
        /// 事务开始
        /// </summary>
        /// <returns></returns>
        public DbTransaction BeginTrans()
        {
            if (!this.inTransaction)
            {
                dbConnection = DbFactory.CreateDbConnection(dbhelper.DbType, dbhelper.ConnectionString);
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                inTransaction = true;
                isOpenTrans = dbConnection.BeginTransaction();
            }
            return isOpenTrans;
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            if (this.inTransaction)
            {
                this.inTransaction = false;
                this.isOpenTrans.Commit();
                this.Close();
            }
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            if (this.inTransaction)
            {
                this.inTransaction = false;
                this.isOpenTrans.Rollback();
                this.Close();
            }
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            if (this.dbConnection != null)
            {
                this.dbConnection.Close();
                this.dbConnection.Dispose();
            }
            if (this.isOpenTrans != null)
            {
                this.isOpenTrans.Dispose();
            }
            this.dbConnection = null;
            this.isOpenTrans = null;
        }
        /// <summary>
        /// 内存回收
        /// </summary>
        public void Dispose()
        {
            if (this.dbConnection != null)
            {
                this.dbConnection.Dispose();
            }
            if (this.isOpenTrans != null)
            {
                this.isOpenTrans.Dispose();
            }
        }
        #endregion

        #region SqlBulkCopy大批量数据插入
        /// <summary>
        /// 大批量数据插入
        /// </summary>
        /// <param name="datatable">资料表</param>
        /// <returns></returns>
        public bool BulkInsert(DataTable datatable)
        {
            return false;
        }
        #endregion

        #region 执行SQL语句
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public int ExecuteBySql(StringBuilder strSql)
        {
            return dbhelper.ExecuteNonQuery(CommandType.Text, strSql.ToString());
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int ExecuteBySql(StringBuilder strSql, DbTransaction isOpenTrans)
        {
            return dbhelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, strSql.ToString());
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public int ExecuteBySql(StringBuilder strSql, DbParameter[] parameters)
        {
            return dbhelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int ExecuteBySql(StringBuilder strSql, DbParameter[] parameters, DbTransaction isOpenTrans)
        {
            return dbhelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, strSql.ToString(), parameters);
        }
        #endregion

        #region 执行存储过程
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <returns></returns>
        public int ExecuteByProc(string procName)
        {
            return dbhelper.ExecuteNonQuery(CommandType.StoredProcedure, procName);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int ExecuteByProc(string procName, DbTransaction isOpenTrans)
        {
            return dbhelper.ExecuteNonQuery(isOpenTrans, CommandType.StoredProcedure, procName);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public int ExecuteByProc(string procName, DbParameter[] parameters)
        {
            return dbhelper.ExecuteNonQuery(CommandType.StoredProcedure, procName, parameters);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int ExecuteByProc(string procName, DbParameter[] parameters, DbTransaction isOpenTrans)
        {
            return dbhelper.ExecuteNonQuery(isOpenTrans, CommandType.StoredProcedure, procName, parameters);
        }
        #endregion

        #region 插入数据
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">实体类对象</param>
        /// <returns></returns>
        public int Insert<T>(T entity)
        {
            object val = 0;
            StringBuilder strSql = DatabaseCommon.InsertSql<T>(dbhelper, entity);
            DbParameter[] parameter = DatabaseCommon.GetParameter<T>(dbhelper,entity);
            val = dbhelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameter);
            return Convert.ToInt32(val);
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">实体类对象</param>
        /// <returns></returns>
        public int Insert<T>(T entity, DbTransaction isOpenTrans, string tablename)
        {
            object val = 0;
            StringBuilder strSql = DatabaseCommon.InsertSql<T>(dbhelper, entity,tablename);
            DbParameter[] parameter = DatabaseCommon.GetParameter<T>(dbhelper, entity);
            val = dbhelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameter);
            return Convert.ToInt32(val);
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">实体类对象</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Insert<T>(T entity, DbTransaction isOpenTrans)
        {
            object val = 0;
            StringBuilder strSql = DatabaseCommon.InsertSql<T>(dbhelper, entity);
            DbParameter[] parameter = DatabaseCommon.GetParameter<T>(dbhelper, entity);
            val = dbhelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, strSql.ToString(), parameter);
            return Convert.ToInt32(val);
        }
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entity">实体类对象</param>
        /// <returns></returns>
        public int Insert<T>(List<T> entity)
        {
            object val = 0;
            DbTransaction isOpenTrans = this.BeginTrans();
            try
            {
                foreach (var item in entity)
                {
                    this.Insert<T>(item, isOpenTrans);
                }
                this.Commit();
                val = 1;
            }
            catch (Exception ex)
            {
                this.Rollback();
                this.Close();
                val = -1;
                throw ex;
            }
            return Convert.ToInt32(val);
        }
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entity">实体类对象</param>
        /// <returns></returns>
        public int Insert<T>(List<T> entity,string tablename)
        {
            object val = 0;
            DbTransaction isOpenTrans = this.BeginTrans();
            try
            {
                foreach (var item in entity)
                {
                    this.Insert<T>(item, isOpenTrans,tablename);
                }
                this.Commit();
                val = 1;
            }
            catch (Exception ex)
            {
                this.Rollback();
                this.Close();
                val = -1;
                throw ex;
            }
            return Convert.ToInt32(val);
        }
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entity">实体类对象</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Insert<T>(List<T> entity, DbTransaction isOpenTrans)
        {
            object val = 0;
            try
            {
                foreach (var item in entity)
                {
                    this.Insert<T>(item, isOpenTrans);
                }
                val = 1;
            }
            catch (Exception ex)
            {
                val = -1;
                throw ex;
            }
            return Convert.ToInt32(val);
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">哈希表键值</param>
        /// <returns></returns>
        public int Insert(string tableName, Hashtable ht)
        {
            object val = 0;
            StringBuilder strSql = DatabaseCommon.InsertSql(dbhelper, tableName, ht);
            DbParameter[] parameter = DatabaseCommon.GetParameter(dbhelper, ht);
            val = dbhelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameter);
            return Convert.ToInt32(val);
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">哈希表键值</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Insert(string tableName, Hashtable ht, DbTransaction isOpenTrans)
        {
            object val = 0;
            StringBuilder strSql = DatabaseCommon.InsertSql(dbhelper, tableName, ht);
            DbParameter[] parameter = DatabaseCommon.GetParameter(dbhelper, ht);
            val = dbhelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, strSql.ToString(), parameter);
            return Convert.ToInt32(val);
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int Update<T>(T entity)
        {
            object val = 0;
            StringBuilder strSql = DatabaseCommon.UpdateSql<T>(dbhelper, entity);
            DbParameter[] parameter = DatabaseCommon.GetParameter<T>(dbhelper, entity);
            val = dbhelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameter);
            return Convert.ToInt32(val);
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Update<T>(T entity, DbTransaction isOpenTrans)
        {
            object val = 0;
            StringBuilder strSql = DatabaseCommon.UpdateSql<T>(dbhelper, entity);
            DbParameter[] parameter = DatabaseCommon.GetParameter<T>(dbhelper, entity);
            val = dbhelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, strSql.ToString(), parameter);
            return Convert.ToInt32(val);
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public int Update<T>(string propertyName, string propertyValue)
        {
            object val = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append("Update ");
            sb.Append(typeof(T).Name);
            sb.Append(" Set ");
            sb.Append(propertyName);
            sb.Append("=");
            sb.Append(dbhelper.DbParmChar + propertyName);
            IList<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter(dbhelper, dbhelper.DbParmChar + propertyName, propertyValue));
            val = dbhelper.ExecuteNonQuery(CommandType.Text, sb.ToString(), parameter.ToArray());
            return Convert.ToInt32(val);
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Update<T>(string propertyName, string propertyValue, DbTransaction isOpenTrans)
        {
            object val = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append("Update ");
            sb.Append(typeof(T).Name);
            sb.Append(" Set ");
            sb.Append(propertyName);
            sb.Append("=");
            sb.Append(dbhelper.DbParmChar + propertyName);
            IList<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter(dbhelper, dbhelper.DbParmChar + propertyName, propertyValue));
            val = dbhelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, sb.ToString(), parameter.ToArray());
            return Convert.ToInt32(val);
        }
        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int Update<T>(List<T> entity)
        {
            object val = 0;
            DbTransaction isOpenTrans = this.BeginTrans();
            try
            {
                foreach (var item in entity)
                {
                    this.Update<T>(item, isOpenTrans);
                }
                this.Commit();
                val = 1;
            }
            catch (Exception ex)
            {
                this.Rollback();
                this.Close();
                val = -1;
                throw ex;
            }
            return Convert.ToInt32(val);
        }
        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Update<T>(List<T> entity, DbTransaction isOpenTrans)
        {
            object val = 0;
            try
            {
                foreach (var item in entity)
                {
                    this.Update<T>(item, isOpenTrans);
                }
                val = 1;
            }
            catch (Exception ex)
            {
                val = -1;
                throw ex;
            }
            return Convert.ToInt32(val);
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">哈希表键值</param>
        /// <param name="propertyName">主键字段</param>
        /// <returns></returns>
        public int Update(string tableName, Hashtable ht, string propertyName)
        {
            object val = 0;
            StringBuilder strSql = DatabaseCommon.UpdateSql(dbhelper, tableName, ht, propertyName);
            DbParameter[] parameter = DatabaseCommon.GetParameter(dbhelper, ht);
            val = dbhelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameter);
            return Convert.ToInt32(val);
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">哈希表键值</param>
        /// <param name="propertyName">主键字段</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Update(string tableName, Hashtable ht, string propertyName, DbTransaction isOpenTrans)
        {
            object val = 0;
            StringBuilder strSql = DatabaseCommon.UpdateSql(dbhelper, tableName, ht, propertyName);
            DbParameter[] parameter = DatabaseCommon.GetParameter(dbhelper, ht);
            val = dbhelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, strSql.ToString(), parameter);
            return Convert.ToInt32(val);
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public int Delete<T>(T entity)
        {
            StringBuilder strSql = DatabaseCommon.DeleteSql(dbhelper,entity);
            DbParameter[] parameter = DatabaseCommon.GetParameter<T>(dbhelper, entity);
            return dbhelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameter);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete<T>(T entity, DbTransaction isOpenTrans)
        {
            StringBuilder strSql = DatabaseCommon.DeleteSql(dbhelper, entity);
            DbParameter[] parameter = DatabaseCommon.GetParameter<T>(dbhelper, entity);
            return dbhelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 批量删除数据modifyby_SY(20190403)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">删除集合</param>
        /// <returns></returns>
        public int Delete<T>(List<T> entity)
        {
            object val = 0;
            DbTransaction isOpenTrans = this.BeginTrans();
            try
            {
                foreach (var item in entity)
                {
                    this.Delete<T>(item, isOpenTrans);
                }
                this.Commit();
                val = 1;
            }
            catch (Exception ex)
            {
                this.Rollback();
                this.Close();
                val = -1;
                throw ex;
            }
            return Convert.ToInt32(val);
        }
        /// <summary>
        /// 批量删除数据modifyby_SY(20190403)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">删除集合</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete<T>(List<T> entity, DbTransaction isOpenTrans)
        {
            object val = 0;
            try
            {
                foreach (var item in entity)
                {
                    this.Delete<T>(item, isOpenTrans);
                }
                val = 1;
            }
            catch (Exception ex)
            {
                val = -1;
                throw ex;
            }
            return Convert.ToInt32(val);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="propertyValue">主键值</param>
        /// <returns></returns>
        public int Delete<T>(object propertyValue)
        {
            string tableName = typeof(T).Name;//获取表名
            string pkName = DatabaseCommon.GetKeyField<T>().ToString();//获取主键
            StringBuilder strSql = DatabaseCommon.DeleteSql(dbhelper, tableName, pkName);
            IList<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter(dbhelper, dbhelper.DbParmChar + pkName, propertyValue));
            return dbhelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="propertyValue">主键值</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete<T>(object propertyValue, DbTransaction isOpenTrans)
        {
            string tableName = typeof(T).Name;//获取表名
            string pkName = DatabaseCommon.GetKeyField<T>().ToString();//获取主键
            StringBuilder strSql = DatabaseCommon.DeleteSql(dbhelper, tableName, pkName);
            IList<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter(dbhelper, dbhelper.DbParmChar + pkName, propertyValue));
            return dbhelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public int Delete<T>(string propertyName, string propertyValue)
        {
            string tableName = typeof(T).Name;//获取表名
            StringBuilder strSql = DatabaseCommon.DeleteSql(dbhelper, tableName, propertyName);
            IList<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter(dbhelper, dbhelper.DbParmChar + propertyName, propertyValue));
            return dbhelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete<T>(string propertyName, string propertyValue, DbTransaction isOpenTrans)
        {
            string tableName = typeof(T).Name;//获取表名
            StringBuilder strSql = DatabaseCommon.DeleteSql(dbhelper, tableName, propertyName);
            IList<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter(dbhelper, dbhelper.DbParmChar + propertyName, propertyValue));
            return dbhelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public int Delete(string tableName, string propertyName, string propertyValue)
        {
            StringBuilder strSql = DatabaseCommon.DeleteSql(dbhelper, tableName, propertyName);
            IList<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter(dbhelper, dbhelper.DbParmChar + propertyName, propertyValue));
            return dbhelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete(string tableName, string propertyName, string propertyValue, DbTransaction isOpenTrans)
        {
            StringBuilder strSql = DatabaseCommon.DeleteSql(dbhelper, tableName, propertyName);
            IList<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter(dbhelper, dbhelper.DbParmChar + propertyName, propertyValue));
            return dbhelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">键值生成SQL条件</param>
        /// <returns></returns>
        public int Delete(string tableName, Hashtable ht)
        {
            StringBuilder strSql = DatabaseCommon.DeleteSql(dbhelper, tableName, ht);
            DbParameter[] parameter = DatabaseCommon.GetParameter(dbhelper, ht);
            return dbhelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameter);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">键值生成SQL条件</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete(string tableName, Hashtable ht, DbTransaction isOpenTrans)
        {
            StringBuilder strSql = DatabaseCommon.DeleteSql(dbhelper, tableName, ht);
            DbParameter[] parameter = DatabaseCommon.GetParameter(dbhelper, ht);
            return dbhelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, strSql.ToString(), parameter);
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="propertyValue">主键值：数组1,2,3,4,5,6.....</param>
        /// <returns></returns>
        public int Delete<T>(object[] propertyValue)
        {
            string tableName = typeof(T).Name;//获取表名
            string pkName = DatabaseCommon.GetKeyField<T>().ToString();//获取主键
            StringBuilder strSql = new StringBuilder("DELETE FROM " + tableName + " WHERE " + pkName + " IN (");
            try
            {
                IList<DbParameter> parameter = new List<DbParameter>();
                int index = 0;
                string str = dbhelper.DbParmChar + "ID" + index;
                for (int i = 0; i < (propertyValue.Length - 1); i++)
                {
                    object obj2 = propertyValue[i];
                    str = dbhelper.DbParmChar + "ID" + index;
                    strSql.Append(str).Append(",");
                    parameter.Add(DbFactory.CreateDbParameter(dbhelper, str, obj2));
                    index++;
                }
                str = dbhelper.DbParmChar + "ID" + index;
                strSql.Append(str);
                parameter.Add(DbFactory.CreateDbParameter(dbhelper, str, propertyValue[index]));
                strSql.Append(")");
                return dbhelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameter.ToArray()); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="propertyValue">主键值：数组1,2,3,4,5,6.....</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete<T>(object[] propertyValue, DbTransaction isOpenTrans)
        {
            string tableName = typeof(T).Name;//获取表名
            string pkName = DatabaseCommon.GetKeyField<T>().ToString();//获取主键
            StringBuilder strSql = new StringBuilder("DELETE FROM " + tableName + " WHERE "  + pkName + " IN (");
            try
            {
                IList<DbParameter> parameter = new List<DbParameter>();
                int index = 0;
                string str = dbhelper.DbParmChar + "ID" + index;
                for (int i = 0; i < (propertyValue.Length - 1); i++)
                {
                    object obj2 = propertyValue[i];
                    str = dbhelper.DbParmChar + "ID" + index;
                    strSql.Append(str).Append(",");
                    parameter.Add(DbFactory.CreateDbParameter(dbhelper, str, obj2));
                    index++;
                }
                str = dbhelper.DbParmChar + "ID" + index;
                strSql.Append(str);
                parameter.Add(DbFactory.CreateDbParameter(dbhelper, str, propertyValue[index]));
                strSql.Append(")");
                return dbhelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, strSql.ToString(), parameter.ToArray()); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6.....</param>
        /// <returns></returns>
        public int Delete<T>(string propertyName, object[] propertyValue)
        {
            string tableName = typeof(T).Name;//获取表名
            string pkName = propertyName;
            StringBuilder strSql = new StringBuilder("DELETE FROM " + tableName + " WHERE " + dbhelper.DbParmChar + pkName + " IN (");
            try
            {
                IList<DbParameter> parameter = new List<DbParameter>();
                int index = 0;
                string str = dbhelper.DbParmChar + "ID" + index;
                for (int i = 0; i < (propertyValue.Length - 1); i++)
                {
                    object obj2 = propertyValue[i];
                    str = dbhelper.DbParmChar + "ID" + index;
                    strSql.Append(str).Append(",");
                    parameter.Add(DbFactory.CreateDbParameter(dbhelper, str, obj2));
                    index++;
                }
                str = dbhelper.DbParmChar + "ID" + index;
                strSql.Append(str);
                parameter.Add(DbFactory.CreateDbParameter(dbhelper, str, propertyValue[index]));
                strSql.Append(")");
                return dbhelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameter.ToArray()); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6.....</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete<T>(string propertyName, object[] propertyValue, DbTransaction isOpenTrans)
        {
            string tableName = typeof(T).Name;//获取表名
            string pkName = propertyName;
            StringBuilder strSql = new StringBuilder("DELETE FROM " + tableName + " WHERE " + pkName + " IN (");
            try
            {
                IList<DbParameter> parameter = new List<DbParameter>();
                int index = 0;
                string str = dbhelper.DbParmChar + "ID" + index;
                for (int i = 0; i < (propertyValue.Length - 1); i++)
                {
                    object obj2 = propertyValue[i];
                    str = dbhelper.DbParmChar + "ID" + index;
                    strSql.Append(str).Append(",");
                    parameter.Add(DbFactory.CreateDbParameter(dbhelper, str, obj2));
                    index++;
                }
                str = dbhelper.DbParmChar + "ID" + index;
                strSql.Append(str);
                parameter.Add(DbFactory.CreateDbParameter(dbhelper, str, propertyValue[index]));
                strSql.Append(")");
                return dbhelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, strSql.ToString(), parameter.ToArray()); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6.....</param>
        /// <returns></returns>
        public int Delete(string tableName, string propertyName, object[] propertyValue)
        {
            string pkName = propertyName;
            StringBuilder strSql = new StringBuilder("DELETE FROM " + tableName + " WHERE " + dbhelper.DbParmChar + pkName + " IN (");
            try
            {
                IList<DbParameter> parameter = new List<DbParameter>();
                int index = 0;
                string str = dbhelper.DbParmChar + "ID" + index;
                for (int i = 0; i < (propertyValue.Length - 1); i++)
                {
                    object obj2 = propertyValue[i];
                    str = dbhelper.DbParmChar + "ID" + index;
                    strSql.Append(str).Append(",");
                    parameter.Add(DbFactory.CreateDbParameter(dbhelper, str, obj2));
                    index++;
                }
                str = dbhelper.DbParmChar + "ID" + index;
                strSql.Append(str);
                parameter.Add(DbFactory.CreateDbParameter(dbhelper, str, propertyValue[index]));
                strSql.Append(")");
                return dbhelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameter.ToArray()); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6.....</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete(string tableName, string propertyName, object[] propertyValue, DbTransaction isOpenTrans)
        {
            string pkName = propertyName;
            StringBuilder strSql = new StringBuilder("DELETE FROM " + tableName + " WHERE " + dbhelper.DbParmChar + pkName + " IN (");
            try
            {
                IList<DbParameter> parameter = new List<DbParameter>();
                int index = 0;
                string str = dbhelper.DbParmChar + "ID" + index;
                for (int i = 0; i < (propertyValue.Length - 1); i++)
                {
                    object obj2 = propertyValue[i];
                    str = dbhelper.DbParmChar + "ID" + index;
                    strSql.Append(str).Append(",");
                    parameter.Add(DbFactory.CreateDbParameter(dbhelper, str, obj2));
                    index++;
                }
                str = dbhelper.DbParmChar + "ID" + index;
                strSql.Append(str);
                parameter.Add(DbFactory.CreateDbParameter(dbhelper, str, propertyValue[index]));
                strSql.Append(")");
                return dbhelper.ExecuteNonQuery(isOpenTrans, CommandType.Text, strSql.ToString(), parameter.ToArray()); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查询数据列表、返回List
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <returns></returns>
        public List<T> FindListTop<T>(int Top) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    strSql.Replace("SELECT", "SELECT TOP " + Top);
                    break;
                case DatabaseType.Oracle:
                    strSql.Append(" and rownum < " + Top + 1);
                    break;
                case DatabaseType.MySql:
                    strSql.Append(" limit " + Top);
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString());
            return DatabaseReader.ReaderToList<T>(dr);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public List<T> FindListTop<T>(int Top, string propertyName, string propertyValue) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(" AND " + propertyName + " = " + dbhelper.DbParmChar + propertyName);
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    strSql.Replace("SELECT", "SELECT TOP " + Top);
                    break;
                case DatabaseType.Oracle:
                    strSql.Append(" and rownum < " + Top + 1);
                    break;
                case DatabaseType.MySql:
                    strSql.Append(" limit " + Top);
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            IList<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter(dbhelper, dbhelper.DbParmChar + propertyName, propertyValue));
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameter.ToArray());
            return DatabaseReader.ReaderToList<T>(dr);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public List<T> FindListTop<T>(int Top, string WhereSql) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(WhereSql);
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    strSql.Replace("SELECT", "SELECT TOP " + Top);
                    break;
                case DatabaseType.Oracle:
                    strSql.Append(" and rownum < " + Top + 1);
                    break;
                case DatabaseType.MySql:
                    strSql.Append(" limit " + Top);
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString());
            return DatabaseReader.ReaderToList<T>(dr);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public List<T> FindListTop<T>(int Top, string WhereSql, DbParameter[] parameters) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(WhereSql);
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    strSql.Replace("SELECT", "SELECT TOP " + Top);
                    break;
                case DatabaseType.Oracle:
                    strSql.Append(" and rownum < " + Top + 1);
                    break;
                case DatabaseType.MySql:
                    strSql.Append(" limit " + Top);
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameters);
            return DatabaseReader.ReaderToList<T>(dr);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <returns></returns>
        public List<T> FindList<T>() where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString());
            return DatabaseReader.ReaderToList<T>(dr);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public List<T> FindList<T>(string propertyName, string propertyValue) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(" AND " + propertyName + " = " + dbhelper.DbParmChar + propertyName);
            IList<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter(dbhelper, dbhelper.DbParmChar + propertyName, propertyValue));
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameter.ToArray());
            return DatabaseReader.ReaderToList<T>(dr);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public List<T> FindList<T>(string WhereSql) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(WhereSql);
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString());
            return DatabaseReader.ReaderToList<T>(dr);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public List<T> FindList<T>(string WhereSql, DbParameter[] parameters) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(WhereSql);
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameters);
            return DatabaseReader.ReaderToList<T>(dr);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public List<T> FindListBySql<T>(string strSql)
        {
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString());
            return DatabaseReader.ReaderToList<T>(dr);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public List<T> FindListBySql<T>(string strSql, DbParameter[] parameters)
        {
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameters);
            return DatabaseReader.ReaderToList<T>(dr);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">返回查询条数</param>
        /// <returns></returns>
        public List<T> FindListPage<T>(string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    return SqlServerHelper.GetPageList<T>(dbhelper, strSql.ToString(), orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.Oracle:
                    return OracleHelper.GetPageList<T>(dbhelper, strSql.ToString(), orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.MySql:
                    return MySqlHelper.GetPageList<T>(dbhelper, strSql.ToString(), orderField, orderType, pageIndex, pageSize, ref recordCount);
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">返回查询条数</param>
        /// <returns></returns>
        public List<T> FindListPage<T>(string WhereSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(WhereSql);
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    return SqlServerHelper.GetPageList<T>(dbhelper, strSql.ToString(), orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.Oracle:
                    return OracleHelper.GetPageList<T>(dbhelper, strSql.ToString(), orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.MySql:
                    return MySqlHelper.GetPageList<T>(dbhelper, strSql.ToString(), orderField, orderType, pageIndex, pageSize, ref recordCount);
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">返回查询条数</param>
        /// <returns></returns>
        public List<T> FindListPage<T>(string WhereSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(WhereSql);
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    return SqlServerHelper.GetPageList<T>(dbhelper,strSql.ToString(), parameters, orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.Oracle:
                    return OracleHelper.GetPageList<T>(dbhelper,strSql.ToString(), parameters, orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.MySql:
                    return MySqlHelper.GetPageList<T>(dbhelper,strSql.ToString(), parameters, orderField, orderType, pageIndex, pageSize, ref recordCount);
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">返回查询条数</param>
        /// <returns></returns>
        public List<T> FindListPageBySql<T>(string strSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount)
        {
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    return SqlServerHelper.GetPageList<T>(dbhelper, strSql, orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.Oracle:
                    return OracleHelper.GetPageList<T>(dbhelper, strSql, orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.MySql:
                    return MySqlHelper.GetPageList<T>(dbhelper, strSql, orderField, orderType, pageIndex, pageSize, ref recordCount);
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">返回查询条数</param>
        /// <returns></returns>
        public List<T> FindListPageBySql<T>(string strSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount)
        {
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    return SqlServerHelper.GetPageList<T>(dbhelper,strSql, parameters, orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.Oracle:
                    return OracleHelper.GetPageList<T>(dbhelper,strSql, parameters, orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.MySql:
                    return MySqlHelper.GetPageList<T>(dbhelper,strSql, parameters, orderField, orderType, pageIndex, pageSize, ref recordCount);
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
        }
        #endregion

        #region 查询数据列表、返回DataTable
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <returns></returns>
        public DataTable FindTableTop<T>(int Top) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    strSql.Replace("SELECT", "SELECT TOP " + Top);
                    break;
                case DatabaseType.Oracle:
                    strSql.Append(" and rownum < " + Top + 1);
                    break;
                case DatabaseType.MySql:
                    strSql.Append(" limit " + Top);
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString());
            return DatabaseReader.ReaderToDataTable(dr);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public DataTable FindTableTop<T>(int Top, string propertyName, string propertyValue) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(" AND " + propertyName + " = " + dbhelper.DbParmChar + propertyName);
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    strSql.Replace("SELECT", "SELECT TOP " + Top);
                    break;
                case DatabaseType.Oracle:
                    strSql.Append(" and rownum < " + Top + 1);
                    break;
                case DatabaseType.MySql:
                    strSql.Append(" limit " + Top);
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            IList<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter(dbhelper, dbhelper.DbParmChar + propertyName, propertyValue));
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameter.ToArray());
            return DatabaseReader.ReaderToDataTable(dr);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public DataTable FindTableTop<T>(int Top, string WhereSql) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(WhereSql);
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    strSql.Replace("SELECT", "SELECT TOP " + Top);
                    break;
                case DatabaseType.Oracle:
                    strSql.Append(" and rownum < " + Top + 1);
                    break;
                case DatabaseType.MySql:
                    strSql.Append(" limit " + Top);
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString());
            return DatabaseReader.ReaderToDataTable(dr);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public DataTable FindTableTop<T>(int Top, string WhereSql, DbParameter[] parameters) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(WhereSql);
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    strSql.Replace("SELECT", "SELECT TOP " + Top);
                    break;
                case DatabaseType.Oracle:
                    strSql.Append(" and rownum < " + Top + 1);
                    break;
                case DatabaseType.MySql:
                    strSql.Append(" limit " + Top);
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameters);
            return DatabaseReader.ReaderToDataTable(dr);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable FindTable<T>() where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString());
            return DatabaseReader.ReaderToDataTable(dr);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public DataTable FindTable<T>(string propertyName, string propertyValue) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(" AND " + propertyName + " = " + dbhelper.DbParmChar + propertyName);
            IList<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter(dbhelper, dbhelper.DbParmChar + propertyName, propertyValue));
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameter.ToArray());
            return DatabaseReader.ReaderToDataTable(dr);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public DataTable FindTable<T>(string WhereSql) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(WhereSql);
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString());
            return DatabaseReader.ReaderToDataTable(dr);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public DataTable FindTable<T>(string WhereSql, DbParameter[] parameters) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(WhereSql);
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameters);
            return DatabaseReader.ReaderToDataTable(dr);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public DataTable FindTableBySql(string strSql)
        {
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString());
            return DatabaseReader.ReaderToDataTable(dr);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public DataTable FindTableBySql(string strSql, DbParameter[] parameters)
        {
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameters);
            return DatabaseReader.ReaderToDataTable(dr);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">返回查询条数</param>
        /// <returns></returns>
        public DataTable FindTablePage<T>(string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    return SqlServerHelper.GetPageTable(dbhelper, strSql.ToString(), orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.Oracle:
                    return OracleHelper.GetPageTable(dbhelper, strSql.ToString(), orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.MySql:
                    return MySqlHelper.GetPageTable(dbhelper, strSql.ToString(), orderField, orderType, pageIndex, pageSize, ref recordCount);
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">返回查询条数</param>
        /// <returns></returns>
        public DataTable FindTablePage<T>(string WhereSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(WhereSql);
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    return SqlServerHelper.GetPageTable(dbhelper, strSql.ToString(), orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.Oracle:
                    return OracleHelper.GetPageTable(dbhelper, strSql.ToString(), orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.MySql:
                    return MySqlHelper.GetPageTable(dbhelper, strSql.ToString(), orderField, orderType, pageIndex, pageSize, ref recordCount);
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">返回查询条数</param>
        /// <returns></returns>
        public DataTable FindTablePage<T>(string WhereSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(WhereSql);
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    return SqlServerHelper.GetPageTable(dbhelper,strSql.ToString(), parameters, orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.Oracle:
                    return OracleHelper.GetPageTable(dbhelper,strSql.ToString(), parameters, orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.MySql:
                    return MySqlHelper.GetPageTable(dbhelper,strSql.ToString(), parameters, orderField, orderType, pageIndex, pageSize, ref recordCount);
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">返回查询条数</param>
        /// <returns></returns>
        public DataTable FindTablePageBySql(string strSql, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount)
        {
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    return SqlServerHelper.GetPageTable(dbhelper,strSql, orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.Oracle:
                    return OracleHelper.GetPageTable(dbhelper,strSql, orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.MySql:
                    return MySqlHelper.GetPageTable(dbhelper,strSql, orderField, orderType, pageIndex, pageSize, ref recordCount);
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="recordCount">返回查询条数</param>
        /// <returns></returns>
        public DataTable FindTablePageBySql(string strSql, DbParameter[] parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int recordCount)
        {
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    return SqlServerHelper.GetPageTable(dbhelper,strSql, parameters, orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.Oracle:
                    return OracleHelper.GetPageTable(dbhelper,strSql, parameters, orderField, orderType, pageIndex, pageSize, ref recordCount);
                case DatabaseType.MySql:
                    return MySqlHelper.GetPageTable(dbhelper,strSql, parameters, orderField, orderType, pageIndex, pageSize, ref recordCount);
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <returns></returns>
        public DataTable FindTableByProc(string procName)
        {
            IDataReader dr = dbhelper.ExecuteReader(CommandType.StoredProcedure, procName);
            return DatabaseReader.ReaderToDataTable(dr);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public DataTable FindTableByProc(string procName, DbParameter[] parameters)
        {
            IDataReader dr = dbhelper.ExecuteReader(CommandType.StoredProcedure, procName, parameters);
            return DatabaseReader.ReaderToDataTable(dr);
        }
        #endregion

        #region 查询数据列表、返回DataSet
        /// <summary>
        /// 查询数据列表、返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public DataSet FindDataSetBySql(string strSql)
        {
            return dbhelper.GetDataSet(CommandType.Text, strSql);
        }
        /// <summary>
        /// 查询数据列表、返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public DataSet FindDataSetBySql(string strSql, DbParameter[] parameters)
        {
            return dbhelper.GetDataSet(CommandType.Text, strSql, parameters);
        }
        /// <summary>
        /// 查询数据列表、返回DataSet
        /// </summary>
        /// <param name="strSql">存储过程</param>
        /// <returns></returns>
        public DataSet FindDataSetByProc(string procName)
        {
            return dbhelper.GetDataSet(CommandType.StoredProcedure, procName);
        }
        /// <summary>
        /// 查询数据列表、返回DataSet
        /// </summary>
        /// <param name="strSql">存储过程</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public DataSet FindDataSetByProc(string procName, DbParameter[] parameters)
        {
            return dbhelper.GetDataSet(CommandType.StoredProcedure, procName, parameters);
        }
        #endregion

        #region 查询对象、返回实体
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="propertyValue">主键值</param>
        /// <returns></returns>
        public T FindEntity<T>(object propertyValue) where T : new()
        {
            string pkName = DatabaseCommon.GetKeyField<T>().ToString();//获取主键字段
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(" AND ").Append(pkName).Append("=").Append(dbhelper.DbParmChar + pkName);
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    strSql.Replace("SELECT", "SELECT TOP " + 1);
                    break;
                case DatabaseType.Oracle:
                    break;
                case DatabaseType.MySql:
                    strSql.Append(" limit 1");
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            IList<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter(dbhelper, dbhelper.DbParmChar + pkName, propertyValue));
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameter.ToArray());
            return DatabaseReader.ReaderToModel<T>(dr);
        }
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public T FindEntity<T>(string propertyName, object propertyValue) where T : new()
        {
            string pkName = propertyName;
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(" AND ").Append(pkName).Append("=").Append(dbhelper.DbParmChar + pkName);
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    strSql.Replace("SELECT", "SELECT TOP " + 1);
                    break;
                case DatabaseType.Oracle:
                    strSql.Append(" and rownum < 2");
                    break;
                case DatabaseType.MySql:
                    strSql.Append(" limit 1");
                    break;
                case DatabaseType.SQLite:
                    strSql.Append(" packid=1 and levelid=1 limit 0,1");
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            IList<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter(dbhelper, dbhelper.DbParmChar + pkName, propertyValue));
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameter.ToArray());
            return DatabaseReader.ReaderToModel<T>(dr);
        }
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public T FindEntityByWhere<T>(string WhereSql) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(WhereSql);
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    strSql.Replace("SELECT", "SELECT TOP " + 1);
                    break;
                case DatabaseType.Oracle:
                    strSql.Append(" and rownum < 2");
                    break;
                case DatabaseType.MySql:
                    strSql.Append(" limit 1");
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString());
            return DatabaseReader.ReaderToModel<T>(dr);
        }
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public T FindEntityByWhere<T>(string WhereSql, DbParameter[] parameters) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectSql<T>();
            strSql.Append(WhereSql);
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    strSql.Replace("SELECT", "SELECT TOP " + 1);
                    break;
                case DatabaseType.Oracle:
                    strSql.Append(" and rownum < 2");
                    break;
                case DatabaseType.MySql:
                    strSql.Append(" limit 1");
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameters);
            return DatabaseReader.ReaderToModel<T>(dr);
        }
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public T FindEntityBySql<T>(string strSql)
        {
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString());
            return DatabaseReader.ReaderToModel<T>(dr);
        }
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public T FindEntityBySql<T>(string strSql, DbParameter[] parameters)
        {
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameters);
            return DatabaseReader.ReaderToModel<T>(dr);
        }
        #endregion

        #region 查询对象、返回哈希表
        /// <summary>
        /// 查询对象、返回哈希表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public Hashtable FindHashtable(string tableName, string propertyName, object propertyValue)
        {
            StringBuilder strSql = DatabaseCommon.SelectSql(tableName);
            strSql.Append(" AND ").Append(propertyName).Append("=").Append(dbhelper.DbParmChar + propertyName);
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    strSql.Replace("SELECT", "SELECT TOP " + 1);
                    break;
                case DatabaseType.Oracle:
                    strSql.Append(" and rownum < 2");
                    break;
                case DatabaseType.MySql:
                    strSql.Append(" limit 1");
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            IList<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter(dbhelper, dbhelper.DbParmChar + propertyName, propertyValue));
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameter.ToArray());
            return DatabaseReader.ReaderToHashtable(dr);
        }
        /// <summary>
        /// 查询对象、返回哈希表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public Hashtable FindHashtable(string tableName, StringBuilder WhereSql)
        {
            StringBuilder strSql = DatabaseCommon.SelectSql(tableName);
            strSql.Append(WhereSql);
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    strSql.Replace("SELECT", "SELECT TOP " + 1);
                    break;
                case DatabaseType.Oracle:
                    strSql.Append(" and rownum < 2");
                    break;
                case DatabaseType.MySql:
                    strSql.Append(" limit 1");
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString());
            return DatabaseReader.ReaderToHashtable(dr);
        }
        /// <summary>
        /// 查询对象、返回哈希表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public Hashtable FindHashtable(string tableName, StringBuilder WhereSql, DbParameter[] parameters)
        {
            StringBuilder strSql = DatabaseCommon.SelectSql(tableName);
            strSql.Append(WhereSql);
            switch (dbhelper.DbType)
            {
                case DatabaseType.SqlServer:
                    strSql.Replace("SELECT", "SELECT TOP " + 1);
                    break;
                case DatabaseType.Oracle:
                    strSql.Append(" and rownum < 2");
                    break;
                case DatabaseType.MySql:
                    strSql.Append(" limit 1");
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameters);
            return DatabaseReader.ReaderToHashtable(dr);
        }
        /// <summary>
        /// 查询对象、返回哈希表
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public Hashtable FindHashtableBySql(string strSql)
        {
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString());
            return DatabaseReader.ReaderToHashtable(dr);
        }
        /// <summary>
        /// 查询对象、返回哈希表
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public Hashtable FindHashtableBySql(string strSql, DbParameter[] parameters)
        {
            IDataReader dr = dbhelper.ExecuteReader(CommandType.Text, strSql.ToString(), parameters);
            return DatabaseReader.ReaderToHashtable(dr);
        }
        #endregion

        #region 查询数据、返回条数
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <returns></returns>
        public int FindCount<T>() where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectCountSql<T>();
            return Convert.ToInt32(dbhelper.ExecuteScalar(CommandType.Text, strSql.ToString()));
        }
        /// <summary>
        /// 查询数据、返回条数
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// </summary>
        /// <returns></returns>
        public int FindCount<T>(string propertyName, string propertyValue) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectCountSql<T>();
            strSql.Append(" AND " + propertyName + " = " + dbhelper.DbParmChar + propertyName);
            IList<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbFactory.CreateDbParameter(dbhelper, dbhelper.DbParmChar + propertyName, propertyValue));
            return Convert.ToInt32(dbhelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameter.ToArray()));
        }
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public int FindCount<T>(string WhereSql) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectCountSql<T>();
            strSql.Append(WhereSql);
            return Convert.ToInt32(dbhelper.ExecuteScalar(CommandType.Text, strSql.ToString()));
        }
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public int FindCount<T>(string WhereSql, DbParameter[] parameters) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectCountSql<T>();
            strSql.Append(WhereSql);
            return Convert.ToInt32(dbhelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters));
        }
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public int FindCountBySql(string strSql)
        {
            return Convert.ToInt32(dbhelper.ExecuteScalar(CommandType.Text, strSql));
        }
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public int FindCountBySql(string strSql, DbParameter[] parameters)
        {
            return Convert.ToInt32(dbhelper.ExecuteScalar(CommandType.Text, strSql, parameters));
        }
        #endregion

        #region 查询数据、返回最大数
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <returns></returns>
        public object FindMax<T>(string propertyName) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectMaxSql<T>(propertyName);
            return dbhelper.ExecuteScalar(CommandType.Text, strSql.ToString());
        }
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public object FindMax<T>(string propertyName, string WhereSql) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectMaxSql<T>(propertyName);
            strSql.Append(WhereSql);
            return dbhelper.ExecuteScalar(CommandType.Text, strSql.ToString());
        }
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public object FindMax<T>(string propertyName, string WhereSql, DbParameter[] parameters) where T : new()
        {
            StringBuilder strSql = DatabaseCommon.SelectMaxSql<T>(propertyName);
            strSql.Append(WhereSql);
            return dbhelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public object FindMaxBySql(string strSql)
        {
            return dbhelper.ExecuteScalar(CommandType.Text, strSql);
        }
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public object FindMaxBySql(string strSql, DbParameter[] parameters)
        {
            return dbhelper.ExecuteScalar(CommandType.Text, strSql, parameters);
        }
        #endregion
    }
}
