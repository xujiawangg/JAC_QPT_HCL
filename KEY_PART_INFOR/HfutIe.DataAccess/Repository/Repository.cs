using HfutIE.DataAccess;
using HfutIe.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HfutIE.Repository
{
    /// <summary>
    /// HfutIE.ORM 定义通用的Repository
    /// <author>
    ///		<name>shecixiong</name>
    ///		<date>2014.02.28</date>
    /// </author>
    /// </summary>
    /// <typeparam name="T">定义泛型，约束其是一个类</typeparam>
    public class Repository<T> : IRepository<T> where T : new()
    {
        private string DbType { get; set; }
        private string DbName { get; set; }
        private string DbConnectionString { get; set; }
        public Repository(string dbType = null, string dbName = null, string dbConnectionString = null)
        {
            DbType = dbType;
            DbName = dbName;
            DbConnectionString = dbConnectionString;
        }
        #region 事务
        /// <summary>
        /// 事务开始
        /// </summary>
        /// <returns></returns>
        public DbTransaction BeginTrans()
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).BeginTrans();
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            DataFactory.Database(DbType, DbName, DbConnectionString).Commit();
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            DataFactory.Database(DbType, DbName, DbConnectionString).Rollback();
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            DataFactory.Database(DbType, DbName, DbConnectionString).Close();
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
            return DataFactory.Database(DbType, DbName, DbConnectionString).BulkInsert(datatable);
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
            return DataFactory.Database(DbType, DbName, DbConnectionString).ExecuteBySql(strSql);
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int ExecuteBySql(StringBuilder strSql, DbTransaction isOpenTrans)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).ExecuteBySql(strSql, isOpenTrans);
        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public int ExecuteBySql(StringBuilder strSql, DbParameter[] parameters)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).ExecuteBySql(strSql, parameters);
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
            return DataFactory.Database(DbType, DbName, DbConnectionString).ExecuteBySql(strSql, parameters, isOpenTrans);
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
            return DataFactory.Database(DbType, DbName, DbConnectionString).ExecuteByProc(procName);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int ExecuteByProc(string procName, DbTransaction isOpenTrans)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).ExecuteByProc(procName, isOpenTrans);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public int ExecuteByProc(string procName, DbParameter[] parameters)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).ExecuteByProc(procName, parameters);
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
            return DataFactory.Database(DbType, DbName, DbConnectionString).ExecuteByProc(procName, parameters, isOpenTrans);
        }
        #endregion

        #region 插入数据
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">实体类对象</param>
        /// <returns></returns>
        public int Insert(T entity)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Insert<T>(entity);
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">实体类对象</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Insert(T entity, DbTransaction isOpenTrans)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Insert<T>(entity, isOpenTrans);
        }
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entity">实体类对象</param>
        /// <returns></returns>
        public int Insert(List<T> entity)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Insert<T>(entity);
        }
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entity">实体类对象</param>
        /// <returns></returns>
        public int Insert(List<T> entity,string tablename)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Insert<T>(entity,tablename);
        }
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entity">实体类对象</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Insert(List<T> entity, DbTransaction isOpenTrans)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Insert<T>(entity, isOpenTrans);
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int Update(T entity)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Update<T>(entity);
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Update(T entity, DbTransaction isOpenTrans)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Update<T>(entity, isOpenTrans);
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public int Update(string propertyName, string propertyValue)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Update<T>(propertyName, propertyValue);
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Update(string propertyName, string propertyValue, DbTransaction isOpenTrans)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Update<T>(propertyName, propertyValue, isOpenTrans);
        }
        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int Update(List<T> entity)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Update<T>(entity);
        }
        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Update(List<T> entity, DbTransaction isOpenTrans)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Update<T>(entity, isOpenTrans);
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
            return DataFactory.Database(DbType, DbName, DbConnectionString).Update(tableName, ht, propertyName);
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
            return DataFactory.Database(DbType, DbName, DbConnectionString).Update(tableName, ht, propertyName, isOpenTrans);
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public int Delete(T entity)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Delete<T>(entity);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete(T entity, DbTransaction isOpenTrans)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Delete<T>(entity, isOpenTrans);
        }

        /// <summary>
        /// 批量删除数据modifyby_SY(20190403)
        /// </summary>
        /// <param name="entity">删除集合</param>
        /// <returns></returns>
        public int Delete(List<T> entity)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Delete<T>(entity);
        }
        /// <summary>
        /// 批量删除数据modifyby_SY(20190403)
        /// </summary>
        /// <param name="entity">删除集合</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete(List<T> entity, DbTransaction isOpenTrans)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Delete<T>(entity,isOpenTrans);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="propertyValue">主键值</param>
        /// <returns></returns>
        public int Delete(object propertyValue)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Delete<T>(propertyValue);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="propertyValue">主键值</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete(object propertyValue, DbTransaction isOpenTrans)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Delete<T>(propertyValue, isOpenTrans);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public int Delete(string propertyName, string propertyValue)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Delete<T>(propertyName, propertyValue);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete(string propertyName, string propertyValue, DbTransaction isOpenTrans)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Delete<T>(propertyName, propertyValue, isOpenTrans);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">键值生成SQL条件</param>
        /// <returns></returns>
        public int Delete(string tableName, Hashtable ht)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Delete(tableName, ht);
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
            return DataFactory.Database(DbType, DbName, DbConnectionString).Delete(tableName, ht, isOpenTrans);
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="propertyValue">主键值：数组1,2,3,4,5,6.....</param>
        /// <returns></returns>
        public int Delete(object[] propertyValue)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Delete<T>(propertyValue);
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="propertyValue">主键值：数组1,2,3,4,5,6.....</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete(object[] propertyValue, DbTransaction isOpenTrans)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Delete<T>(propertyValue, isOpenTrans);
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6.....</param>
        /// <returns></returns>
        public int Delete(string propertyName, object[] propertyValue)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Delete<T>(propertyName, propertyValue);
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6.....</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete(string propertyName, object[] propertyValue, DbTransaction isOpenTrans)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).Delete<T>(propertyName, propertyValue, isOpenTrans);
        }
        #endregion

        #region 查询数据列表、返回List
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <returns></returns>
        public List<T> FindListTop(int Top)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindListTop<T>(Top);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public List<T> FindListTop(int Top, string propertyName, string propertyValue)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindListTop<T>(Top, propertyName, propertyValue);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public List<T> FindListTop(int Top, string WhereSql)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindListTop<T>(Top, WhereSql);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public List<T> FindListTop(int Top, string WhereSql, DbParameter[] parameters)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindListTop<T>(Top, WhereSql, parameters);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <returns></returns>
        public List<T> FindList()
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindList<T>();
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public List<T> FindList(string propertyName, string propertyValue)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindList<T>(propertyName, propertyValue);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public List<T> FindList(string WhereSql)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindList<T>(WhereSql);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public List<T> FindList(string WhereSql, DbParameter[] parameters)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindList<T>(WhereSql, parameters);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public List<T> FindListBySql(string strSql)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindListBySql<T>(strSql);
        }
        /// <summary>
        /// 查询数据列表、返回List
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public List<T> FindListBySql(string strSql, DbParameter[] parameters)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindListBySql<T>(strSql, parameters);
        }
        #endregion

        #region 查询数据列表、返回DataTable
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <returns></returns>
        public DataTable FindTableTop(int Top)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindTableTop<T>(Top);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public DataTable FindTableTop(int Top, string propertyName, string propertyValue)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindTableTop<T>(Top, propertyName, propertyValue);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public DataTable FindTableTop(int Top, string WhereSql)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindTableTop<T>(Top, WhereSql);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="Top">显示条数</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public DataTable FindTableTop(int Top, string WhereSql, DbParameter[] parameters)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindTableTop<T>(Top, WhereSql, parameters);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable FindTable()
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindTable<T>();
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public DataTable FindTable(string propertyName, string propertyValue)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindTable<T>(propertyName, propertyValue);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public DataTable FindTable(string WhereSql)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindTable<T>(WhereSql);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public DataTable FindTable(string WhereSql, DbParameter[] parameters)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindTable<T>(WhereSql, parameters);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public DataTable FindTableBySql(string strSql)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindTableBySql(strSql);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public DataTable FindTableBySql(string strSql, DbParameter[] parameters)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindTableBySql(strSql, parameters);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <returns></returns>
        public DataTable FindTableByProc(string procName)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindTableByProc(procName);
        }
        /// <summary>
        /// 查询数据列表、返回 DataTable
        /// </summary>
        /// <param name="procName">存储过程</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public DataTable FindTableByProc(string procName, DbParameter[] parameters)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindTableByProc(procName, parameters);
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
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindDataSetBySql(strSql);
        }
        /// <summary>
        /// 查询数据列表、返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public DataSet FindDataSetBySql(string strSql, DbParameter[] parameters)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindDataSetBySql(strSql, parameters);
        }
        /// <summary>
        /// 查询数据列表、返回DataSet
        /// </summary>
        /// <param name="strSql">存储过程</param>
        /// <returns></returns>
        public DataSet FindDataSetByProc(string procName)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindDataSetByProc(procName);
        }
        /// <summary>
        /// 查询数据列表、返回DataSet
        /// </summary>
        /// <param name="strSql">存储过程</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public DataSet FindDataSetByProc(string procName, DbParameter[] parameters)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindDataSetByProc(procName, parameters);
        }
        #endregion

        #region 查询对象、返回实体
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="propertyValue">主键值</param>
        /// <returns></returns>
        public T FindEntity(object propertyValue)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindEntity<T>(propertyValue);
        }
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// <returns></returns>
        public T FindEntity(string propertyName, object propertyValue)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindEntity<T>(propertyName, propertyValue);
        }
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public T FindEntityByWhere(string WhereSql)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindEntityByWhere<T>(WhereSql);
        }
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public T FindEntityByWhere(string WhereSql, DbParameter[] parameters)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindEntityByWhere<T>(WhereSql, parameters);
        }
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public T FindEntityBySql(string strSql)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindEntityBySql<T>(strSql);
        }
        /// <summary>
        /// 查询对象、返回实体
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public T FindEntityBySql(string strSql, DbParameter[] parameters)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindEntityBySql<T>(strSql, parameters);
        }
        #endregion

        #region 查询数据、返回条数
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <returns></returns>
        public int FindCount()
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindCount<T>();
        }
        /// <summary>
        /// 查询数据、返回条数
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值</param>
        /// </summary>
        /// <returns></returns>
        public int FindCount(string propertyName, string propertyValue)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindCount<T>(propertyName, propertyValue);
        }
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public int FindCount(string WhereSql)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindCount<T>(WhereSql);
        }
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public int FindCount(string WhereSql, DbParameter[] parameters)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindCount<T>(WhereSql, parameters);
        }
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public int FindCountBySql(string strSql)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindCountBySql(strSql);
        }
        /// <summary>
        /// 查询数据、返回条数
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public int FindCountBySql(string strSql, DbParameter[] parameters)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindCountBySql(strSql, parameters);
        }
        #endregion

        #region 查询数据、返回最大数
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <returns></returns>
        public object FindMax(string propertyName)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindMax<T>(propertyName);
        }
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="WhereSql">条件</param>
        /// <returns></returns>
        public object FindMax(string propertyName, string WhereSql)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindMax<T>(propertyName, WhereSql);
        }
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="WhereSql">条件</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public object FindMax(string propertyName, string WhereSql, DbParameter[] parameters)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindMax<T>(propertyName, WhereSql, parameters);
        }
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public object FindMaxBySql(string strSql)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindMaxBySql(strSql);
        }
        /// <summary>
        /// 查询数据、返回最大数
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns></returns>
        public object FindMaxBySql(string strSql, DbParameter[] parameters)
        {
            return DataFactory.Database(DbType, DbName, DbConnectionString).FindMaxBySql(strSql, parameters);
        }
        #endregion
    }
}
