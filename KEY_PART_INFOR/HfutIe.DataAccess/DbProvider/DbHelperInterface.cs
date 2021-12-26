using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace HfutIE.DataAccess
{
    /// <summary>
    /// 数据库操作基类
    /// </summary>
    public class DbHelperInterface
    {
        /// <summary>
        /// 调试日志
        /// </summary>
       // public static Log log = LogFactory.GetLogger(typeof(DbHelper));

        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnectionString { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public static string DbType { get; set; }
        /// <summary>
        /// 数据库命名参数符号
        /// </summary>
        public static string DbParmChar { get; set; }
        public DbHelperInterface(string connstring)
        {
            //string ConStringDESEncrypt = ConfigurationManager.AppSettings["ConStringDESEncrypt"];
            //ConnectionString = ConfigurationManager.ConnectionStrings[connstring].ConnectionString;
            //if (ConStringDESEncrypt == "true")
            //{
            //    ConnectionString = DESEncrypt.Decrypt(ConnectionString);
            //}
            //this.DatabaseTypeEnumParse(ConfigurationManager.ConnectionStrings[connstring].ProviderName);
            //DbParmChar = DbFactory.CreateDbParmCharacter();
        }
        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数。
        /// </summary>
        /// <param name="isOpenTrans">事务对象</param>
        /// <param name="cmdType">执行命令的类型（存储过程或T-SQL，等等）</param>
        /// <param name="cmdText">存储过程名称或者T-SQL命令行</param>
        /// <param name="parameters">执行命令所需的sql语句对应参数</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(DbTransaction isOpenTrans, CommandType cmdType, string cmdText, DbConnection conn)
        {
            int num = 0;
            try
            {
                DbCommand cmd = CreateDbCommand();
                if (isOpenTrans == null || isOpenTrans.Connection == null)
                {
                    PrepareCommand(cmd, conn, isOpenTrans, cmdType, cmdText, null);
                    num = cmd.ExecuteNonQuery();
                }
                else
                {
                    PrepareCommand(cmd, isOpenTrans.Connection, isOpenTrans, cmdType, cmdText, null);
                    num = cmd.ExecuteNonQuery();
                }
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                num = -1;
                //log.Error(ex.Message);
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
        public static IDataReader ExecuteReader(CommandType cmdType, string cmdText, DbConnection conn)
        {
            DbCommand cmd = CreateDbCommand();
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
                //log.Error(ex.Message);
                throw;
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
        private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction isOpenTrans, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            //cmd.CommandTimeout = int.Parse(ConfigurationManager.AppSettings["CommandTimeout"]);
            if (isOpenTrans != null)
                cmd.Transaction = isOpenTrans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                cmd.Parameters.AddRange(cmdParms);
            }
        }
        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库命令对象
        /// </summary>
        /// <returns></returns>
        private static DbCommand CreateDbCommand()
        {
            DbCommand cmd = null;
            switch (DbType)
            {
                case "SqlServer":
                    cmd = new SqlCommand();
                    break;
                case "Oracle":
                    OracleCommand cmd_oracle = new OracleCommand();
                    cmd_oracle.BindByName = true;
                    cmd = cmd_oracle;
                    break;
                case "MySql":
                    cmd = new MySqlCommand();
                    break;
                case "Access":
                    cmd = new OleDbCommand();
                    break;
                case "SQLite":
                    cmd = new SQLiteCommand();
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
            return cmd;
        }
    }
}
