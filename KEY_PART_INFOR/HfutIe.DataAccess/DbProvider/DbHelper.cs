using HfutIe;
using HfutIe.DataAccess.Common;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace HfutIE.DataAccess
{
    /// <summary>
    /// 数据库操作基类
    /// </summary>
    public class DbHelper
    {
        /// <summary>
        /// 调试日志
        /// </summary>
        private Log log = Log.GetInstance;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseType DbType { get; set; }
        /// <summary>
        /// 数据库命名参数符号
        /// </summary>
        public string DbParmChar { get; set; }
        public DbHelper(string connName)
        {
            string ConStringDESEncrypt = ConfigurationManager.AppSettings["ConStringDESEncrypt"];
            ConnectionString = ConfigurationManager.ConnectionStrings[connName].ConnectionString;
            if (ConStringDESEncrypt == "true")
            {
                ConnectionString = DESEncrypt.Decrypt(ConnectionString);
            }
            this.DatabaseTypeEnumParse(ConfigurationManager.ConnectionStrings[connName].ProviderName);
            DbParmChar = DbFactory.CreateDbParmCharacter(DbType);
        }
        public DbHelper(string connString,string dbType)
        {
            ConnectionString = connString;
            DbType = GetDatabaseTypeByString(dbType);
            DbParmChar = DbFactory.CreateDbParmCharacter(DbType);
        }
        private DatabaseType GetDatabaseTypeByString(string dbType)
        {
            DatabaseType result;
            if (Enum.TryParse(dbType, out result))
            {
                return result;
            }
            throw new ArgumentException("数据库类型输入不合法：" + dbType);
        }
        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数。
        /// </summary>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <returns></returns>
        public int ExecuteNonQuery(CommandType cmdType, string cmdText)
        {
            int num = 0;
            DbCommand cmd = DbFactory.CreateDbCommand(DbType);
            try
            {
                using (DbConnection conn = DbFactory.CreateDbConnection(DbType, ConnectionString))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, null);
                    num = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                num = -1;
                log.Error(ex.Message);
                throw;
            }
            finally
            {
                cmd.Dispose();
            }
            return num;
        }
        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数。
        /// </summary>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <param name="parameters">执行命令所需的sql语句对应参数</param>
        /// <returns></returns>
        public int ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            int num = 0;
            DbCommand cmd = DbFactory.CreateDbCommand(DbType);
            try
            {
                using (DbConnection conn = DbFactory.CreateDbConnection(DbType, ConnectionString))
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, parameters);
                    num = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                num = -1;
                log.Error(ex.Message);
                throw;
            }
            finally
            {
                cmd.Dispose();
            }
            return num;
        }
        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数。
        /// </summary>
        /// <param name="isOpenTrans">事务对象</param>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <param name="parameters">执行命令所需的sql语句对应参数</param>
        /// <returns></returns>
        public int ExecuteNonQuery(DbTransaction isOpenTrans, CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            int num = 0;
            try
            {
                DbCommand cmd = DbFactory.CreateDbCommand(DbType);
                if (isOpenTrans == null || isOpenTrans.Connection == null)
                {
                    using (DbConnection conn = DbFactory.CreateDbConnection(DbType, ConnectionString))
                    {
                        PrepareCommand(cmd, conn, isOpenTrans, cmdType, cmdText, parameters);
                        num = cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    PrepareCommand(cmd, isOpenTrans.Connection, isOpenTrans, cmdType, cmdText, parameters);
                    num = cmd.ExecuteNonQuery();
                }
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                num = -1;
                log.Error(ex.Message);
                throw;
            }
            return num;
        }
        /// <summary>
        ///使用提供的参数，执行有结果集返回的数据库操作命令、并返回SqlDataReader对象
        /// </summary>
        /// <param name="commandType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="commandText">存储过程名称或者T-SQL命令行<</param>
        /// <returns>返回SqlDataReader对象</returns>
        public IDataReader ExecuteReader(CommandType cmdType, string cmdText)
        {
            DbCommand cmd = DbFactory.CreateDbCommand(DbType);
            DbConnection conn = DbFactory.CreateDbConnection(DbType, ConnectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, null);
                IDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception ex)
            {
                conn.Close();
                cmd.Dispose();
                log.Error(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// 使用提供的参数，执行有结果集返回的数据库操作命令、并返回SqlDataReader对象
        /// </summary>
        /// <param name="commandType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="commandText">存储过程名称或者T-SQL命令行<</param>
        /// <param name="parameters">执行命令所需的sql语句对应参数</param>
        /// <returns>返回SqlDataReader对象</returns>
        public IDataReader ExecuteReader(CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            DbCommand cmd = DbFactory.CreateDbCommand(DbType);
            DbConnection conn = DbFactory.CreateDbConnection(DbType, ConnectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, parameters);
                IDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception ex)
            {
                conn.Close();
                cmd.Dispose();
                log.Error(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// 查询数据填充到数据集DataSet中
        /// </summary>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="parameters">sql语句对应参数</param>
        /// <returns>数据集DataSet对象</returns>
        public DataSet GetDataSet(CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            DataSet ds = new DataSet();
            DbCommand cmd = DbFactory.CreateDbCommand(DbType);
            DbConnection conn = DbFactory.CreateDbConnection(DbType, ConnectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, parameters);
                IDbDataAdapter sda = DbFactory.CreateDataAdapter(DbType, cmd);
                sda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                conn.Close();
                cmd.Dispose();
                log.Error(ex.Message);
                throw;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
        }
        /// <summary>
        /// 查询数据填充到数据集DataSet中
        /// </summary>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">命令文本</param>
        /// <returns>数据集DataSet对象</returns>
        public DataSet GetDataSet(CommandType cmdType, string cmdText)
        {
            DataSet ds = new DataSet();
            DbCommand cmd = DbFactory.CreateDbCommand(DbType);
            DbConnection conn = DbFactory.CreateDbConnection(DbType, ConnectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, null);
                IDbDataAdapter sda = DbFactory.CreateDataAdapter(DbType, cmd);
                sda.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                conn.Close();
                cmd.Dispose();
                log.Error(ex.Message);
                throw;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
        }
        /// <summary>
        /// 依靠数据库连接字符串connectionString,
        /// 使用所提供参数，执行返回首行首列命令
        /// </summary>
        /// <param name="commandType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="commandText">存储过程名称或者T-SQL命令行</param>
        /// <param name="parameters">执行命令所需的sql语句对应参数</param>
        /// <returns>返回一个对象，使用Convert.To{Type}将该对象转换成想要的数据类型。</returns>
        public object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] parameters)
        {
            DbCommand cmd = DbFactory.CreateDbCommand(DbType);
            try
            {
                using (DbConnection connection = DbFactory.CreateDbConnection(DbType, ConnectionString))
                {
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, parameters);
                    object val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }
            finally
            {
                cmd.Dispose();
            }
        }
        /// <summary>
        /// 依靠数据库连接字符串connectionString,
        /// 使用所提供参数，执行返回首行首列命令
        /// </summary>
        /// <param name="commandType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="commandText">存储过程名称或者T-SQL命令行</param>
        /// <returns>返回一个对象，使用Convert.To{Type}将该对象转换成想要的数据类型。</returns>
        public object ExecuteScalar(CommandType cmdType, string cmdText)
        {
            DbCommand cmd = DbFactory.CreateDbCommand(DbType);
            try
            {
                using (DbConnection connection = DbFactory.CreateDbConnection(DbType, ConnectionString))
                {
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, null);
                    object val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }
            finally
            {
                cmd.Dispose();
            }
        }
        /// <summary>
        /// 为即将执行准备一个命令
        /// </summary>
        /// <param name="cmd">SqlCommand对象</param>
        /// <param name="conn">SqlConnection对象</param>
        /// <param name="isOpenTrans">DbTransaction对象</param>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行, e.g. Select * from Products</param>
        /// <param name="cmdParms">SqlParameters to use in the command</param>
        private void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction isOpenTrans, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = int.Parse(ConfigurationManager.AppSettings["CommandTimeout"]);
            if (isOpenTrans != null)
                cmd.Transaction = isOpenTrans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                cmd.Parameters.AddRange(cmdParms);
            }
        }
        /// <summary>
        /// 用于数据库类型的字符串枚举转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public void DatabaseTypeEnumParse(string value)
        {
            try
            {
                switch (value)
                {
                    case "System.Data.SqlClient":
                        DbType = DatabaseType.SqlServer;
                        break;
                    case "System.Data.OracleClient":
                        DbType = DatabaseType.Oracle;
                        break;
                    case "MySql.Data.MySqlClient":
                        DbType = DatabaseType.MySql;
                        break;
                    case "System.Data.OleDb":
                        DbType = DatabaseType.Access;
                        break;
                    case "System.Data.SQLite":
                        DbType = DatabaseType.SQLite;
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                throw new Exception("数据库类型\"" + value + "\"错误，请检查！");
            }
        }
    }
}
