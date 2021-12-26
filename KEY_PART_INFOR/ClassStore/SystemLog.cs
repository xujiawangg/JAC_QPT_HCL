using System;
using System.Data;
using System.Collections;
using log4net;
using HfutIE.Repository;
using HfutIE.Entity;

namespace HfutIe
{
    class SystemLog
    {
        #region 仓储
        static RepositoryFactory<SYSTEM_CS> SYSTEM_CSRepositoryFactory = new RepositoryFactory<SYSTEM_CS>();//底层操作日志表
        #endregion

        #region 属性
        /// <summary>
        /// 用户编号
        /// </summary>
        public static string UserCode = "";//用户编号
        /// <summary>
        /// 用户key
        /// </summary>
        public static string UserKey = "";//用户key
        /// <summary>
        /// 用户姓名
        /// </summary>
        public static string UserName = "";//姓名
        /// <summary>
        /// 进入哪一个程序
        /// </summary>
        public static String  InOrOut = "";//
        /// <summary>
        /// 当前计算机名
        /// </summary>
        public static string PCname = "";//计算机名
        /// <summary>
        /// 当前计算机IP
        /// </summary>
        public static string IP = "";//IP地址
        public static string IPCode = "";//服务器IP端口
        public static string personalRolesName = "";
        public static string LoginName = "";
        public static string QualityStationKey = "";//质量门配置工位主键
        public static ArrayList personalPowerList;
        /// <summary>
        /// 当前用户权限列表
        /// </summary>
        public static ArrayList RightList = new ArrayList();//权限表
        #endregion

        #region 方法
        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="ct">操作内容</param>
        /// <param name="OperateType">操作类型（添加，修改，删除等等）</param>
        public static void AddLog(string ct)
        {

            SYSTEM_CS system_cs = new SYSTEM_CS();
            system_cs.USER_CODE = UserCode;
            system_cs.USER_NAME = UserName;
            system_cs.CT = ct;
            system_cs.PC_NAME = PCname;
            system_cs.IP = IP;
            system_cs.Create();
            SYSTEM_CSRepositoryFactory.Repository().Insert(system_cs);
        }

        /// <summary>
        /// 判断是否有操作权限
        /// </summary>
        /// <param name="RightIP">权限编号</param>
        /// <returns></returns>
        public static bool RightJudge(string RightIP)
        {
            return true;
        }

        public static ArrayList getPowerList(string personalID)
        {
            ArrayList list = new ArrayList();
            if (personalID == "")
            {
                return list;
            }

            string sqlStr = "";
            DataTable dt = new DataTable();
            try
            {
                sqlStr = "select a.right_key,a.right_code,a.right_name from RIGHT_INFO a,RIGHT_ROLE b,RIGHT_USER_ROLE c where b.role_key=c.role_key and a.right_key=b.right_key and c.user_key='" + personalID + "'";
                DataSet ds = DbHelperSQL.Query(sqlStr);
                 dt = ds.Tables[0];
                //dt = DbHelperSQL.OpenTable(sqlStr);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        list.Add(dt.Rows[i]["right_code"].ToString());
                    }
                }
                sqlStr = "";
                dt = new DataTable();
                sqlStr = "select a.right_name,a.right_code,a.right_key from RIGHT_INFO a,RIGHT_USER b where a.right_key=b.right_key and b.user_key='" + personalID + "'";
                dt = DbHelperSQL.OpenTable(sqlStr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (list.Contains(dt.Rows[i]["right_code"].ToString()))
                    {
                        continue;
                    }
                    else
                    {
                        list.Add(dt.Rows[i]["right_code"].ToString());
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("加载失败！", ex);
                list = new ArrayList();
                return list;
            }
        }
        #endregion
    }
}
