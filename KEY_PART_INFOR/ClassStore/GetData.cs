using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using data;
using System.Windows.Forms;
using System.Drawing;
using System.IO.Ports;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using log4net;
using System.Configuration;
using HfutIE.Repository;
using HfutIE.Entity;
using KEY_PART_INFOR;
using MsgBox;

namespace HfutIe
{
    public class GetData
    {
        public static RepositoryFactory<Part_Supplier> SupplierRepositoryFactory = new RepositoryFactory<Part_Supplier>();//供应商基本信息表
        public static RepositoryFactory<P_ASSEMBLE_PRODUCT_STATE> P_ASSEMBLE_PRODUCT_STATERepositoryFactory = new RepositoryFactory<P_ASSEMBLE_PRODUCT_STATE>();//生产状态过程表
        public static RepositoryFactory<BasicInfoDto> BasicInfoDtoRepositoryFactory = new RepositoryFactory<BasicInfoDto>();//基本信息
        public static RepositoryFactory<EQUIPMENT> EquipmentRepositoryFactory = new RepositoryFactory<EQUIPMENT>();//设备基本信息
        public static RepositoryFactory<CHECK_INFO> CheckInfoRepositoryFactory = new RepositoryFactory<CHECK_INFO>();//设备点检项基本信息
        public static RepositoryFactory<CHECK_STATION_INFO> CheckStationInfoRepositoryFactory = new RepositoryFactory<CHECK_STATION_INFO>();//工位点检项配置基本信息
       
        public static DataTable GetWcWorkNum(string wc_code)
        {

            string sql = "select Is_Ok,count(product_born_code)as Num from DOC_EQUIP_TIME_INFO where wc_code='" + wc_code + "' group by Is_Ok";
            DataTable dt = DbHelperSQL.OpenTable(sql);
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
                return null;
        }
        /// <summary>
        /// 获取当前工位名称
        /// </summary>
        /// <returns></returns>
        public static string GetWc_Code()
        {
            string wc_code = ConfigurationManager.AppSettings.Get("wc_code");
            return wc_code;
        }
        #region 获取工厂建模信息
        /// <summary>
        /// 根据工位号获取工厂信息，返回设备KEY，设备编号，设备名称，工位KEY,工位编号，工位名称,生产线key，生产线编号，生产线名称，
        /// 生产车间KEY，生产车间编号，生产车间名称，工厂KEY，工厂编号，工厂名称,公司KEY ,公司编号，公司名称
        /// 
        /// </summary>
        /// <param name="wc_code">工位编号</param>
        /// <returns></returns>
        public static BasicInfoDto GetFactoryInforByWccode(string StationCode)
        {
            BasicInfoDto basicinfo = new BasicInfoDto();
            try
            {
                if (StationCode!=null)
                {
                    string SqlStr = "select S.SITE_KEY,S.SITE_CODE,S.SITE_NAME, A.AREA_KEY,A.AREA_CODE,A.AREA_NAME,WS.WORKSHOP_KEY,WS.WORKSHOP_CODE,WS.WORKSHOP_NAME,MC.MACHINING_CENTER_KEY,MC.MACHINING_CENTER_CODE,MC.MACHINING_CENTER_NAME,PL.PRODUCTION_LINE_KEY,PL.PRODUCTION_LINE_CODE,PL.PRODUCTION_LINE_NAME,WC.WORK_CENTER_KEY ,WC.WORK_CENTER_CODE,WC.WORK_CENTER_NAME,STA.STATION_KEY,STA.STATION_CODE,STA.STATION_NAME,E.EQUIPMENT_KEY,E.EQUIPMENT_CODE,E.EQUIPMENT_NAME ";
                    SqlStr = SqlStr + " from SITE S RIGHT JOIN AREA A ON A.SITE_KEY = S.SITE_KEY ";
                    SqlStr = SqlStr + " RIGHT JOIN WORKSHOP WS ON WS.AREA_KEY = A.AREA_KEY ";
                    SqlStr = SqlStr + " RIGHT JOIN MACHINING_CENTER MC ON MC.WORKSHOPKEY = WS.WORKSHOP_KEY";
                    SqlStr = SqlStr + " RIGHT JOIN PRODUCTION_LINE PL ON PL.MACHINING_CENTER_KEY = MC.MACHINING_CENTER_KEY RIGHT JOIN WORK_CENTER WC ON WC.PRODUCTION_LINE_KEY = PL.PRODUCTION_LINE_KEY ";
                    SqlStr = SqlStr + " RIGHT JOIN STATION STA ON STA.WORK_CENTER_KEY = WC.WORK_CENTER_KEY";
                    SqlStr = SqlStr + " LEFT JOIN EQUIPMENT E ON E.STATION_KEY = STA.STATION_KEY";
                    //SqlStr = SqlStr + " where  STA.STATION_CODE='" + wc_code + "'";
                    basicinfo = BasicInfoDtoRepositoryFactory.Repository().FindListBySql(SqlStr).Where(s=> s.STATION_CODE== StationCode).FirstOrDefault();
                }
                return basicinfo;
            }
            catch (Exception ex)
            {
                //MyMsgBox.Show("工厂信息获取失败！", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("工厂信息获取失败！" + ex.Message);
                return basicinfo;
            }
        }
        /// <summary>
        /// 根据工位号获取设备信息，返回设备KEY，设备编号，设备名称，工位KEY,工位编号，工位名称
        /// </summary>
        /// <param name="wc_code">工位编号</param>
        /// <returns></returns>
        public static List<EQUIPMENT> GetEquipInforByWCcode(List<string> StationCodeList)
        {
            List<EQUIPMENT> equipmentinfo = new List<EQUIPMENT>();
            if (StationCodeList.Count>0)
            {
                equipmentinfo = EquipmentRepositoryFactory.Repository().FindList().Where(s=> StationCodeList.Contains(s.STATION_CODE)).ToList();
            }
            return equipmentinfo;
        }
        /// <summary>
        /// 根据工位号，获取生产线信息，返回生产线key，生产线编号，生产线名称，工位KEY，工位编号，工位名称
        /// </summary>
        /// <param name="wc_code">工位编号</param>
        /// <returns></returns>
        public static DataTable GetProlineInforByWCcode(string wc_code)
        {
            DataTable dt = new DataTable();
            List<ProductionLineInfoDto> lineinfo = new List<ProductionLineInfoDto>();
            if (!System.String.IsNullOrEmpty(wc_code))
            {
                string SqlStr = " select P.PRODUCTION_LINE_KEY,P.PRODUCTION_LINE_CODE,P.PRODUCTION_LINE_NAME , WC.WORK_CENTER_KEY, WC.WORK_CENTER_CODE, WC.WORK_CENTER_NAME,S.STATION_KEY, S.STATION_CODE, S.STATION_NAME ";
                SqlStr = SqlStr + "  from PRODUCTION_LINE P RIGHT JOIN WORK_CENTER WC ON WC.PRODUCTION_LINE_KEY = P.PRODUCTION_LINE_KEY RIGHT JOIN STATION S ON S.WORK_CENTER_KEY = WC.WORK_CENTER_KEY";
                SqlStr = SqlStr + "  where  WC.wc_code='" + wc_code + "' ";
                dt = DbHelperSQL.OpenTable(SqlStr);
            }
            return dt;
        }
        /// <summary>
        /// 根据生产线key,获取生产车间信息，返回生产车间KEY，生产车间编号，生产车间名称，生产线KEY，生产线编号，生产线名称
        /// </summary>
        /// <param name="p_line_key">生产线ID</param>
        /// <returns></returns>
        public static DataTable GetWorkShopInforByProlinekey(string p_line_key)
        {
            DataTable dt = new DataTable();
            if (!System.String.IsNullOrEmpty(p_line_key))
            {
                string SqlStr = " select WS.ws_key,WS.ws_code,WS.ws_name,P.p_line_key ,P.p_line_code,P.p_line_name  ";
                SqlStr = SqlStr + " from WORKSHOP WS RIGHT JOIN WS_P_LINE_LIST WSL ON WSL.ws_key = WSL.ws_key RIGHT JOIN Productionline P ON P.p_line_key = WSL.p_line_key ";
                SqlStr = SqlStr + " where  P.p_line_key='" + p_line_key + "' ";
                dt = DbHelperSQL.OpenTable(SqlStr);
            }
            return dt;
        }
        /// <summary>
        /// 根据车间KEY，获取工厂信息，返回工厂KEY，工厂编号，工厂名称，车间KEY，车间编号，车间名称
        /// </summary>
        /// <param name="ws_key">车间ID</param>
        /// <returns></returns>
        public static DataTable GetAreaInforByWorkShopkey(string ws_key)
        {
            DataTable dt = new DataTable();
            if (!System.String.IsNullOrEmpty(ws_key))
            {
                string SqlStr = " select  A.area_key,A.area_code,A.area_name,WS.ws_key,WS.ws_code,WS.ws_name  ";
                SqlStr = SqlStr + " from AREA A RIGHT JOIN AREA_WS_LIST AL ON A.area_key = AL.area_key LEFT JOIN WORKSHOP WS ON WS.ws_key = AL.ws_key ";
                SqlStr = SqlStr + " where  WS.ws_key='" + ws_key + "' ";
                dt = DbHelperSQL.OpenTable(SqlStr);
            }
            return dt;
        }
        /// <summary>
        /// 根据工厂KEY，获取公司信息，返回公司KEY ,公司编号，公司名称，工厂KEY，工厂编号，工厂名称
        /// </summary>
        /// <param name="Area_key">工厂ID</param>
        /// <returns></returns>
        static DataTable GetSiteInforByAreakey(string Area_key)
        {
            DataTable dt = new DataTable();
            if (!System.String.IsNullOrEmpty(Area_key))
            {
                string SqlStr = "select S.site_key,S.site_code,S.site_name, A.area_key,A.area_code,A.area_name ";
                SqlStr = SqlStr + " from SITE S LEFT join SITE_AREA_LIST ST on S.site_key = ST.site_key LEFT JOIN AREA A ON A.area_key = ST.area_key ";
                SqlStr = SqlStr + " where  A.area_key='" + Area_key + "' ";
                dt = DbHelperSQL.OpenTable(SqlStr);
            }
            return dt;
        }
        #endregion
        #region 获取班制，班组，人员信息
        /// <summary>
        ///根据工位ID， 获取当前时间的班制，班组，人员信息
        /// </summary>
        /// <param name="wc_key"></param>
        /// <returns></returns>
        public static DataTable GetTeamByWccode(string wc_code)
        {
            DataTable dt = new DataTable();
            DateTime time = ServerTime.Now;
            string timedate = time.ToShortDateString();
            string timenow = time.ToShortTimeString();
            try
            {
                if (!System.String.IsNullOrEmpty(wc_code))
                {
                    string SqlStr = "select shift_key,shift_code,shift_name,team_key,team_code,team_name ,staff_key ,staff_code ,staff_name,start_time,end_time ";
                    SqlStr = SqlStr + " from P_SCHEDULING  where date ='" + timedate + "' and wc_code='" + wc_code + "' order by start_time ";
                    dt = DbHelperSQL.OpenTable(SqlStr);
                    if (dt.Rows.Count > 1)
                    {
                        DataTable newdt = dt.Clone();
                        DataRow[] dr = dt.Select("start_time<'" + timenow + "'  and end_time>'" + timenow + "'");

                        for (int i = 0; i < dr.Length; i++)
                        {
                            newdt.ImportRow((DataRow)dr[i]);
                        }
                        return newdt;

                    }
                    else
                    {
                        return dt;
                    }
                }
                else
                {
                    dt = null;
                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("班组信息获取失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return dt;
            }
        }
        #endregion
        /// <summary>
        /// 根据产品出生证，从产品信息过程表中获取产品基本信息，返回工厂建模信息，计划信息，产品信息
        /// </summary>
        /// <param name="product_born_code">产品出生证</param>
        /// <returns></returns>
        public static List<P_ASSEMBLE_PRODUCT_STATE> GetProductinforByborncode(string product_born_code)
        {
            List<P_ASSEMBLE_PRODUCT_STATE> list = new List<P_ASSEMBLE_PRODUCT_STATE>();
            try
            {
                if (!System.String.IsNullOrEmpty(product_born_code))
                {
                    //list = P_ASSEMBLE_PRODUCT_STATERepositoryFactory.Repository().FindList().Where(s => s.PRODUCT_BORN_CODE == product_born_code).ToList();
                    list = P_ASSEMBLE_PRODUCT_STATERepositoryFactory.Repository().FindList($" and PRODUCT_BORN_CODE='{product_born_code}'");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("产品信息获取失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("产品信息获取失败！" + ex.Message);
            }
            return list;
        }
        /// <summary>
        /// 根据零部件编号，获取零部件信息，返回零部件ID，零部件编号，零部件名称,零部件类型。
        /// </summary>
        /// <param name="part_code">零部件编号</param>
        /// <returns></returns>
        public static DataTable GetPartInforByPartCode(string part_code)
        {
            DataTable dt = new DataTable();
            try
            {
                if (!System.String.IsNullOrEmpty(part_code))
                {
                    string SqlStr = "select part_key,part_code,part_name,part_type from PART  where  part_code='" + part_code + "'";
                    dt = DbHelperSQL.OpenTable(SqlStr);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("零件信息获取失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("零件信息获取失败！" + ex.Message);
            }
            return dt;
        }
        /// <summary>
        /// 查询供应商信息,返回供应商ID，供应商名称，供应商编号
        /// </summary>
        /// <param name="part_code">零部件编号</param>
        /// <returns></returns>
        public static List<Part_Supplier> GetSupplierInfor()
        {
            List<Part_Supplier> supplierlist = new List<Part_Supplier>();
            try
            {
                //获取所有供应商信息
                supplierlist = SupplierRepositoryFactory.Repository().FindList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("供应商信息获取失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("供应商信息获取失败！" + ex.Message);
            }
            return supplierlist;
        }
        /// <summary>
        /// 查询工位配置的设备点检信息
        /// </summary>
        /// <param name="part_code">零部件编号</param>
        /// <returns></returns>
        public static List<CHECK_INFO> GetCheckInforByStationKey(string StationKey)
        {
            List<CHECK_INFO> checkinfolist = new List<CHECK_INFO>();
            try
            {
                List<CHECK_STATION_INFO> checkstationlist = CheckStationInfoRepositoryFactory.Repository().FindList().Where(s => s.STATION_KEY == StationKey).ToList();
                checkinfolist = CheckInfoRepositoryFactory.Repository().FindList().Where(s=> checkstationlist.Select(x=>x.CHECK_INFO_KEY).Contains(s.CHECK_INFO_KEY)).OrderBy(s=>s.CHECK_INFO_CODE).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("设备点检信息获取失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("设备点检信息获取失败！" + ex.Message);
            }
            return checkinfolist;
        }

        
    }
}
