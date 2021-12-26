using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HfutIe
{
    /// <summary>
    /// 操作数据库的方法
    /// create by LY
    /// </summary>
    class DataBaseOpByEntity
    {
        #region 获取对象参数值
        /// <summary>
        /// 对象参数转换DbParameter
        /// </summary>
        /// <returns></returns>
        private SqlParameter[] GetParameter<T>(T entity)
        {
            IList<SqlParameter> parameter = new List<SqlParameter>();
            DbType dbtype = new DbType();
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo pi in props)
            {
                if (pi.GetValue(entity, null) != null)
                {
                    switch (pi.PropertyType.ToString())
                    {
                        case "System.Nullable`1[System.Int32]":
                            dbtype = DbType.Int32;
                            break;
                        case "System.Nullable`1[System.Decimal]":
                            dbtype = DbType.Decimal;
                            break;
                        case "System.Nullable`1[System.DateTime]":
                            dbtype = DbType.DateTime;
                            break;
                        default:
                            dbtype = DbType.String;
                            break;
                    }
                    SqlParameter param = new SqlParameter();
                    param.DbType = dbtype;
                    param.ParameterName = "@" + pi.Name;
                    param.Value = pi.GetValue(entity, null);
                    parameter.Add(param);
                }
            }
            return parameter.ToArray();
        }
        #endregion

        #region 插入数据
        /// <summary>
        /// 泛型方法，反射生成InsertSql语句
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns>int</returns>
        private StringBuilder InsertSql<T>(T entity)
        {
            Type type = entity.GetType();
            StringBuilder sb = new StringBuilder();
            sb.Append(" Insert Into ");
            sb.Append(type.Name);
            sb.Append("(");
            StringBuilder sp = new StringBuilder();
            StringBuilder sb_prame = new StringBuilder();
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(entity, null) != null)
                {
                    sb_prame.Append("," + (prop.Name));
                    sp.Append(",@" + "" + (prop.Name));
                }
            }
            sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") Values (");
            sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")");
            return sb;
        }


        /// <summary>
        /// 根据实体对象向数据库插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public  int Insert<T>(T entity)
        {
            int a = 0;
            try
            {
                StringBuilder sb = InsertSql<T>(entity);
                SqlParameter[] parms = GetParameter<T>(entity);
                a = data.DBQuery.ExecuteSql(sb.ToString(), parms);
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("插入数据失败！" + ex.Message);
            }
            return a;
        }


        /// <summary>
        /// 使用事务将实体插入数据库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">实体</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public  int Insert<T>(T entity,DbTransaction tran)
        {
            int a = 0;
            try
            {
                StringBuilder sb = InsertSql<T>(entity);
                SqlParameter[] parms = GetParameter<T>(entity);
                a = ExecuteNonQuery(tran, sb.ToString(), parms);
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("插入数据失败！" + ex.Message);
                throw;
            }
            return a;
        }



        /// <summary>
        /// 批量插入数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitylist">数据集合</param>
        /// <returns></returns>
        public int Insert<T>(List<T> entitylist)
        {
            int a = 0;
            DbTransaction tran = BeginTrans();
            try
            {
                foreach (T entity in entitylist)
                {
                    Insert<T>(entity, tran);
                }
                tran.Commit();
                a = 1;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("插入数据失败！" + ex.Message);
            }
            return a;
        }
        /// <summary>
        /// 批量插入数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitylist">数据集合</param>
        /// <param name="tran">事务</param>
        /// <returns></returns>
        public int Insert<T>(List<T> entitylist,DbTransaction tran)
        {
            int a = 0;
            //DbTransaction tran = BeginTrans();
            try
            {
                foreach (T entity in entitylist)
                {
                    Insert<T>(entity, tran);
                }
                a = 1;
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("插入数据失败！" + ex.Message);
            }
            return a;
        }
        #endregion

        #region 修改数据

        #region 更新数据字符串拼接方法
        
        /// <summary>
        /// 泛型方法，反射生成UpdateSql语句
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="pkName">主键</param>
        /// <returns>int</returns>
        private StringBuilder UpdateSql<T>(T entity, string pkName)
        {
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            StringBuilder sb = new StringBuilder();
            sb.Append(" Update ");
            sb.Append(type.Name);
            sb.Append(" Set ");
            bool isFirstValue = true;
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(entity, null) != null && pkName != prop.Name)
                {
                    if (isFirstValue)
                    {
                        isFirstValue = false;
                        sb.Append(prop.Name);
                        sb.Append("=");
                        sb.Append("@" + prop.Name);
                    }
                    else
                    {
                        sb.Append("," + prop.Name);
                        sb.Append("=");
                        sb.Append("@" + prop.Name);
                    }
                }
            }
            sb.Append(" Where ").Append(pkName).Append("=").Append("@" + pkName);
            return sb;
        }
        /// <summary>
        /// 泛型方法，反射生成UpdateSql语句
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns>int</returns>
        public  StringBuilder UpdateSql<T>(T entity)
        {
            string pkName = GetKeyField<T>().ToString();
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            StringBuilder sb = new StringBuilder();
            sb.Append("Update ");
            sb.Append(type.Name);
            sb.Append(" Set ");
            bool isFirstValue = true;
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(entity, null) != null && pkName != prop.Name)
                {
                    if (isFirstValue)
                    {
                        isFirstValue = false;
                        sb.Append(prop.Name);
                        sb.Append("=");
                        sb.Append("@" + prop.Name);
                    }
                    else
                    {
                        sb.Append("," + prop.Name);
                        sb.Append("=");
                        sb.Append("@" + prop.Name);
                    }
                }
            }
            sb.Append(" Where ").Append(pkName).Append("=").Append("@" + pkName);
            return sb;
        }
        #endregion

        /// <summary>
        /// 根据实体修改数据库数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="pkName">主键字段名</param>
        /// <returns></returns>
        public int Update<T>(T entity, string pkName)
        {
            int a = 0;
            try
            {
                StringBuilder sb = UpdateSql<T>(entity, pkName);
                SqlParameter[] parms = GetParameter<T>(entity);
                a = data.DBQuery.ExecuteSql(sb.ToString(), parms);
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("修改数据失败！" + ex.Message);
            }
            return a;
        }

        /// <summary>
        /// 根据实体修改数据库数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="pkName">主键字段名</param>
        /// <returns></returns>
        public int Update<T>(T entity, string pkName, DbTransaction isOpenTrans)
        {
            int a = 0;
            try
            {
                StringBuilder sb = UpdateSql<T>(entity, pkName);
                SqlParameter[] parms = GetParameter<T>(entity);
                a = ExecuteNonQuery(isOpenTrans, sb.ToString(), parms);
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("修改数据失败！" + ex.Message);
            }
            return a;
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public  int Update<T>(T entity)
        {
            object val = 0;
            try
            {
                StringBuilder strSql = UpdateSql<T>(entity);
                SqlParameter[] parameter = GetParameter<T>(entity);
                val = data.DBQuery.ExecuteSql(strSql.ToString(), parameter);
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("修改数据失败！" + ex.Message);
            }
            return Convert.ToInt32(val);
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public  int Update<T>(T entity, DbTransaction isOpenTrans)
        {
            object val = 0;
            try
            {
                StringBuilder strSql = UpdateSql<T>(entity);
                SqlParameter[] parameter = GetParameter<T>(entity);
                val = ExecuteNonQuery(isOpenTrans, strSql.ToString(), parameter);
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("修改数据失败！" + ex.Message);
            }
            return Convert.ToInt32(val);
        }
        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public  int Update<T>(List<T> entity)
        {
            object val = 0;
            DbTransaction isOpenTrans = BeginTrans();
            try
            {
                foreach (var item in entity)
                {
                    Update<T>(item, isOpenTrans);
                }
                isOpenTrans.Commit();
                val = 1;
            }
            catch (Exception ex)
            {
                isOpenTrans.Rollback();
                isOpenTrans.Dispose();
                val = -1;
                throw ex;
            }
            return Convert.ToInt32(val);
        }
        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isOpenTrans">事务</param>
        /// <returns></returns>
        public  int Update<T>(List<T> entity,DbTransaction isOpenTrans)
        {
            object val = 0;
            //DbTransaction isOpenTrans = this.BeginTrans();
            try
            {
                foreach (var item in entity)
                {
                    Update<T>(item, isOpenTrans);
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
        #endregion

        #region 删除数据
        /// <summary>
        /// 拼接删除SQL语句
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        private StringBuilder DeleteSql<T>(T entity)
        {
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            StringBuilder sb = new StringBuilder("Delete From " + type.Name + " Where 1=1");
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(entity, null) != null)
                {
                    sb.Append(" AND " + prop.Name + " = @" + "" + prop.Name + "");
                }
            }
            return sb;
        }
        /// <summary>
        /// 根据实体删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public  int Delete<T>(T entity)
        {
            int a = 0;
            try
            {
                StringBuilder sb = DeleteSql<T>(entity);
                SqlParameter[] parms = GetParameter<T>(entity);
                a = data.DBQuery.ExecuteSql(sb.ToString(), parms);
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("删除数据失败！" + ex.Message);
            }
            return a;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <returns></returns>
        public int Delete<T>(T entity, DbTransaction isOpenTrans)
        {
            StringBuilder strSql = DeleteSql(entity);
            DbParameter[] parameter = GetParameter<T>(entity);
            return ExecuteNonQuery(isOpenTrans, strSql.ToString(), parameter.ToArray());
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
            StringBuilder strSql = new StringBuilder("DELETE FROM " + tableName + " WHERE " + "@" + pkName + " IN (");
            try
            {
                IList<SqlParameter> parameter = new List<SqlParameter>();
                int index = 0;
                string str = "@" + "ID" + index;
                for (int i = 0; i < (propertyValue.Length - 1); i++)
                {
                    object obj2 = propertyValue[i];
                    str = "@" + "ID" + index;
                    strSql.Append(str).Append(",");
                    parameter.Add(CreateDbParameter(str, obj2));
                    index++;
                }
                str = "@" + "ID" + index;
                strSql.Append(str);
                parameter.Add(CreateDbParameter(str, propertyValue[index]));
                strSql.Append(")");
                return data.DBQuery.ExecuteSql(strSql.ToString(), parameter.ToArray()); ;
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
                string str = "@" + "ID" + index;
                for (int i = 0; i < (propertyValue.Length - 1); i++)
                {
                    object obj2 = propertyValue[i];
                    str = "@" + "ID" + index;
                    strSql.Append(str).Append(",");
                    parameter.Add(CreateDbParameter(str, obj2));
                    index++;
                }
                str = "@" + "ID" + index;
                strSql.Append(str);
                parameter.Add(CreateDbParameter(str, propertyValue[index]));
                strSql.Append(")");
                return ExecuteNonQuery(isOpenTrans,  strSql.ToString(), parameter.ToArray()); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 带有事务的执行方法

        /// <summary>
        /// 带有事务的实际操作数据库的方法
        /// </summary>
        /// <param name="isOpenTrans"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(DbTransaction isOpenTrans, string cmdText, params DbParameter[] parameters)
        {
            int num = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                if (isOpenTrans == null || isOpenTrans.Connection == null)
                {
                    using (DbConnection conn = new SqlConnection(data.data.ConnString))
                    {
                        PrepareCommand(cmd, conn, isOpenTrans, cmdText, parameters);
                        num = cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    PrepareCommand(cmd, isOpenTrans.Connection, isOpenTrans, cmdText, parameters);
                    num = cmd.ExecuteNonQuery();
                }
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                num = -1;
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal(ex.Message);
                throw;
            }
            return num;
        }

        /// <summary>
        /// 准备请求与连接，对command的参数进行设定
        /// </summary>
        /// <param name="cmd">command对象</param>
        /// <param name="conn">connection对象</param>
        /// <param name="isOpenTrans">事务对象</param>
        /// <param name="cmdText">command执行语句</param>
        /// <param name="cmdParms">command执行语句参数</param>
        private void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction isOpenTrans, string cmdText, DbParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = int.Parse("5000");
            if (isOpenTrans != null)
                cmd.Transaction = isOpenTrans;
            if (cmdParms != null)
            {
                cmd.Parameters.AddRange(cmdParms);
            }
        }

        /// <summary>
        /// 数据库连接
        /// </summary>
        private DbConnection dbConnection { get; set; }
        /// <summary>
        /// 事务对象
        /// </summary>
        private DbTransaction isOpenTrans { get; set; }

        /// <summary>
        /// 连接是否已在事务中
        /// </summary>
        public bool inTransaction { get; set; }



        /// <summary>
        /// 事务开始
        /// </summary>
        /// <returns></returns>
        public  DbTransaction BeginTrans()
        {
            //DbTransaction isOpenTrans = null;
            if (!inTransaction)
            {
                dbConnection = new SqlConnection(data.data.ConnString);
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
        public  void Commit()
        {
            if (inTransaction)
            {
                inTransaction = false;
                isOpenTrans.Commit();
                Close();
            }
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public  void Rollback()
        {
            if (inTransaction)
            {
                inTransaction = false;
                isOpenTrans.Rollback();
                Close();
            }
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public  void Close()
        {
            if (dbConnection != null)
            {
                dbConnection.Close();
                dbConnection.Dispose();
            }
            if (isOpenTrans != null)
            {
                isOpenTrans.Dispose();
            }
            dbConnection = null;
            isOpenTrans = null;
        }
        /// <summary>
        /// 内存回收
        /// </summary>
        public void Dispose()
        {
            if (dbConnection != null)
            {
                dbConnection.Dispose();
            }
            if (isOpenTrans != null)
            {
                isOpenTrans.Dispose();
            }
        }
        #endregion

        #region 获取主键

        public  object GetKeyField<T>()
        {
            Type objTye = typeof(T);
            string _KeyField = "";
            PrimaryKeyAttribute KeyField;
            var name = objTye.Name;
            foreach (Attribute attr in objTye.GetCustomAttributes(true))
            {
                KeyField = attr as PrimaryKeyAttribute;
                if (KeyField != null)
                    _KeyField = KeyField.Name;
            }
            return _KeyField;
        }
        #endregion

        #region 数据库执行语句的参数

        /// <summary>
        /// 根据配置文件中所配置的数据库类型
        /// 来创建相应数据库的参数对象
        /// </summary>
        /// <returns></returns>
        public  SqlParameter CreateDbParameter(string paramName, object value)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = paramName;
            param.Value = value;
            return param;
        }
        #endregion

        #region 插入外部数据库 从外部数据库检索
        
        /// <summary>
        /// 将实体数据插入外部数据库
        /// </summary>
        /// <typeparam name="T">实体泛型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="connection">外部数据库连接</param>
        /// <returns></returns>
        public  int InsertToOtherDataBase<T>(T entity, string connection)
        {
            int num = 0;
            StringBuilder cmdText = InsertSql(entity);
            SqlParameter[] sqlparameters = GetParameter(entity);
            DbCommand cmd = new SqlCommand();
            try
            {
                using (DbConnection conn = new SqlConnection(connection))
                {
                    PrepareCommand(cmd, conn, null, CommandType.Text, cmdText.ToString(), sqlparameters);
                    num = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                num = -1;
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("插入数据失败！" + ex.Message);
            }
            finally
            {
                cmd.Dispose();
            }
            return num;
        }
        /// <summary>
        /// 外部数据库查询数据
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public DataTable FindEntity(string SQLString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQLString, connection);
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    da.Dispose();
                    connection.Close();
                }

                return dt;
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
            cmd.CommandTimeout = 5000;
            if (isOpenTrans != null)
                cmd.Transaction = isOpenTrans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                cmd.Parameters.AddRange(cmdParms);
            }
        }
        #endregion

        #region 从外部数据库查询信息
         
        /// <summary>
        /// 从给定连接的数据库查询数据，返回object（调用此方法后可自行转化处理数据）
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <param name="connectionString">数据库连接</param>
        /// <returns></returns>
        public object FindObject(string SQLString,string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))// <param name="SQLString">计算查询结果语句</param>
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;//查询结果
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }
        #endregion
    }
}
