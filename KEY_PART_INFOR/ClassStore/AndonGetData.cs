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
using KEY_PART_INFOR;
using HfutIE.Entity;
using HfutIE.Repository;

namespace HfutIe
{
    public class AndonGetData
    {
        static RepositoryFactory<AndonInfoDto> AndonInfoDtoFactory = new RepositoryFactory<AndonInfoDto>();//工位信息
        static RepositoryFactory<MATERIAL_WC_PART> MATERIAL_WC_PARTFactory = new RepositoryFactory<MATERIAL_WC_PART>();//工位线边库信息
        static RepositoryFactory<P_ANDON_INFOR> P_ANDON_INFORFactory = new RepositoryFactory<P_ANDON_INFOR>();//andon过程信息表
        static RepositoryFactory<P_ANDON_MATERIAL_NEED> P_ANDON_MATERIAL_NEEDFactory = new RepositoryFactory<P_ANDON_MATERIAL_NEED>();//andon物料需求过程信息表

        #region 获取工厂建模信息
        /// <summary>
        /// 根据工位号获取工厂信息，返回设备KEY，设备编号，设备名称，工位KEY,工位编号，工位名称,生产线key，生产线编号，生产线名称，
        /// 生产车间KEY，生产车间编号，生产车间名称，工厂KEY，工厂编号，工厂名称,公司KEY ,公司编号，公司名称
        /// 
        /// </summary>
        /// <param name="wc_code">工位编号</param>
        /// <returns></returns>
        public static DataTable GetFactoryInforByWccode(string wc_code)
        {
            DataTable dt = new DataTable();
            try
            {
                if (!System.String.IsNullOrEmpty(wc_code))
                {
                    string SqlStr = "select S.site_key,S.site_code,S.site_name, A.area_key,A.area_code,A.area_name,WS.ws_key,WS.ws_code,WS.ws_name,P.p_line_key,WC.wc_key ,P.p_line_code,P.p_line_name,WC.wc_code,WC.wc_name,E.equip_code,E.equip_name ";
                    SqlStr = SqlStr + " from SITE S right join SITE_AREA_LIST ST on S.site_key = ST.site_key RIGHT JOIN AREA A ON A.area_key = ST.area_key ";
                    SqlStr = SqlStr + " RIGHT JOIN   AREA_WS_LIST AL ON A.area_key = AL.area_key RIGHT JOIN WORKSHOP WS ON WS.ws_key = AL.ws_key ";
                    SqlStr = SqlStr + " RIGHT JOIN WS_P_LINE_LIST WSL ON WSL.ws_key = WSL.ws_key RIGHT JOIN Productionline P ON P.p_line_key = WSL.p_line_key ";
                    SqlStr = SqlStr + " RIGHT JOIN P_LINE_WC_LIST PL ON PL.p_line_key = P.p_line_key RIGHT JOIN WORK_CENTER WC ON WC.wc_key = PL.wc_key ";
                    SqlStr = SqlStr + " RIGHT JOIN WC_EQUIPMENT_LIST L ON WC.wc_key = L.wc_key RIGHT JOIN EQUIPMENT E on E.equip_key = L.equip_key ";
                    SqlStr = SqlStr + " where  WC.wc_code='" + wc_code + "'";
                    dt = DbHelperSQL.OpenTable(SqlStr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("工厂信息获取失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("工厂信息获取失败！" + ex.Message);
                return dt;
            }
        }
        /// <summary>
        /// 根据工位号获取设备信息，返回设备KEY，设备编号，设备名称，工位KEY,工位编号，工位名称
        /// </summary>
        /// <param name="wc_code">工位编号</param>
        /// <returns></returns>
        public static DataTable GetEquipInforByWCcode(string wc_code)
        {
            DataTable dt = new DataTable();
            if (!System.String.IsNullOrEmpty(wc_code))
            {
                string SqlStr = "select E.equip_key,E.equip_code,E.equip_name, W.wc_key, W.wc_code, W.wc_name ";
                SqlStr = SqlStr + "  from EQUIPMENT E LEFT JOIN WC_EQUIPMENT_LIST L ON E.equip_key = L.equip_key LEFT JOIN WORK_CENTER W on W.wc_key = L.wc_key";
                SqlStr = SqlStr + "  where  W.wc_code='" + wc_code + "' ";
                dt = DbHelperSQL.OpenTable(SqlStr);
            }
            return dt;
        }
        /// <summary>
        /// 根据工位号，获取生产线信息，返回生产线key，生产线编号，生产线名称，工位KEY，工位编号，工位名称
        /// </summary>
        /// <param name="wc_code">工位编号</param>
        /// <returns></returns>
        public static DataTable GetProlineInforByWCcode(string wc_code)
        {
            DataTable dt = new DataTable();
            if (!System.String.IsNullOrEmpty(wc_code))
            {
                string SqlStr = " select P.p_line_key,P.p_line_code,P.p_line_name , WC.wc_key, WC.wc_code, WC.wc_name ";
                SqlStr = SqlStr + "  from Productionline P RIGHT JOIN P_LINE_WC_LIST PL ON PL.p_line_key = P.p_line_key RIGHT JOIN WORK_CENTER WC ON WC.wc_key = PL.wc_key";
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
        public static DataTable GetSiteInforByAreakey(string Area_key)
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
                        try
                        {
                            for (int i = 0; i < dr.Length; i++)
                            {
                                newdt.ImportRow((DataRow)dr[i]);
                            }
                            return newdt;
                        }
                        catch (Exception ex1)
                        {
                            throw ex1;
                        }
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
        public static DataTable GetProductinforByborncode(string product_born_code)
        {
            DataTable dt = new DataTable();
            try
            {
                if (!System.String.IsNullOrEmpty(product_born_code))
                {
                    string SqlStr = "select site_key,site_code,site_name,area_key,area_code,area_name,ws_key,ws_code,ws_name,p_line_key,p_line_code,p_line_name,";
                    SqlStr = SqlStr + "MES_plan_code,plan_num,bom_key,";
                    SqlStr = SqlStr + " product_key,product_born_code,product_code,product_name,product_name,product_abb,product_batch_no,product_born_code,product_model_no,product_serial_no,product_struct_no,product_type ";
                    SqlStr = SqlStr + " from P_ASSEMBLE_PRODUCT_STATE  where  product_born_code='" + product_born_code + "'";
                    dt = DbHelperSQL.OpenTable(SqlStr);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("产品信息获取失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("产品信息获取失败！" + ex.Message);
            }
            return dt;
        }

        #region 获取基本信息，计划信息
        /// <summary>
        /// 根据产品出生证，从产品信息过程表中获取工厂基本信息，计划信息
        /// </summary>
        /// <param name="product_born_code">产品出生证</param>
        /// <returns></returns>
        public static DataTable GetPlanDatabyProductBornCode(string product_born_code)
        {
            DataTable dt = new DataTable();
            try
            {
                if (!System.String.IsNullOrEmpty(product_born_code))
                {
                    //string SqlStr = "select MES_plan_code,plan_num,bom_key,product_code,product_name,product_batch_no";
                    //SqlStr = SqlStr + " from P_ASSEMBLE_PRODUCT_STATE  where  product_born_code='" + product_born_code + "'";
                    string SqlStr = "select MES_plan_code,plan_num,product_key,product_code,product_name,product_batch_no, product_abb , product_batch_no , product_born_code , product_model_no , product_serial_no , product_struct_no , product_type , distribution_ws_key , distribution_ws_code , distribution_ws_name ";//从产品加工状态表中获取计划信息
                    SqlStr = SqlStr + " from P_ASSEMBLE_PRODUCT_STATE  where  product_born_code='" + product_born_code + "'";
                    dt = DbHelperSQL.OpenTable(SqlStr);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("计划信息获取失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("计划信息获取失败！" + ex.Message);
            }
            return dt;
        }
        #endregion

        #region 获取线边库信息
        public static DataTable GetMaterialWcPart(string wc_key, string part_key)
        {
            DataTable dt = new DataTable();
            try
            {
                if (!System.String.IsNullOrEmpty(wc_key) && !System.String.IsNullOrEmpty(part_key))
                {
                    string SqlStr = "select  material_wc_part_key , wc_key , part_key ,  safety_num , storage_num  FROM Material_WC_Part ";
                    SqlStr = SqlStr + "   where  wc_key='" + wc_key + "' and part_key='" + part_key + "'";
                    dt = DbHelperSQL.OpenTable(SqlStr);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("线边库库存信息获取失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("线边库库存信息获取失败！" + ex.Message);
            }
            return dt;
        }
        public static List<MATERIAL_WC_PART> GetMaterialWcPart(string wc_key)
        {
            List<MATERIAL_WC_PART> ma_wc_partlist = new List<MATERIAL_WC_PART>();
            DataTable dt = new DataTable();
            try
            {
                if (!System.String.IsNullOrEmpty(wc_key))
                {
                    //1.首先，获取当前工位最近加工产品的装配上线时间
                    //2.获取大于该装配时间的所有产品信息，即比此产品后加工的产品
                    //3.获取比此产品更后加工的产品所属的所有生产计划
                    //4.从安全件采集表中，获取所有有关生产计划需要采集的零部件数据
                    //5.从上述零部件信息中，获取需要在本工位采集的零部件信息，与线边库连接查询出线边库存信息

                    ma_wc_partlist = MATERIAL_WC_PARTFactory.Repository().FindList().Where(s => s.STATION_KEY == wc_key).ToList();
//                    string SqlStr = @"
//select material_wc_part_key,wc_key,KEY_PART_C.part_key,part_code,part_name,max_num,safety_num,delivery_unit_num,storage_num,remark from 
//(
//	select material_wc_part_key,wc_key,Material_WC_Part.part_key,part_code,part_name,max_num,safety_num,delivery_unit_num,storage_num,remark FROM Material_WC_Part with(nolock) where wc_key='" + wc_key + @"' 
//) 
//material_wc_part join 
//(
//    select distinct part_key from KEY_PART_C join
//    (
//        select distinct mes_plan_key from
//        (
//            select top 30 * from P_ASSEMBLE_PRODUCT_STATE where assemble_online_time > =
//           (
//                select case when max(assemble_online_time) is NULL then GETDATE() else max(assemble_online_time) end from P_ASSEMBLE_PRODUCT_STATE where wc_key = '" + wc_key + @"'
//           ) 
//            order by assemble_online_time desc
//        ) T
//    ) mes_plan_key on mes_plan_key.mes_plan_key=KEY_PART_C.mes_plan_key
//) KEY_PART_C on KEY_PART_C.part_key = material_wc_part.part_key";
//                    dt = DbHelperSQL.OpenTable(SqlStr);
//                    if (dt != null && dt.Rows.Count == 0)//如果上述语句查询不到数据，从各产线中按照生产计划执行顺序加载需要采集的安全件
//                    {
//                        SqlStr = @"select  material_wc_part_key , wc_key , part_key, part_code, part_name ,  max_num,  safety_num,  delivery_unit_num , storage_num , remark  FROM Material_WC_Part   with(readpast)   where  wc_key='" + wc_key + @"' and part_key in 
//(
//    select distinct part_key from KEY_PART_C where mes_plan_key in
//    (
//		select top 10 mes_plan_key from P_PLAN where p_line_name=
//		(
//			select case when left((
//									select wc_code from WORK_CENTER where wc_key='" + wc_key + @"'
//								  ),1)='Q' 
//			then '轻卡线' else '重卡线' end)order by execution_queue_no asc
//	)
//)";
//                        dt = DbHelperSQL.OpenTable(SqlStr);
//                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("线边库库存信息获取失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("线边库库存信息获取失败！" + ex.Message);
            }
            return ma_wc_partlist;
        }
        #endregion

        #region 获取安全件采集配置信息
        /// <summary>
        /// 从安全件信息采集配置表中获得需要采集的安全件信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetKeyPartConfig()
        {
            DataTable dt = new DataTable();
            try
            {
                //从安全件采集配置表中获取需要采集的安全件信息
                string SqlStr = "SELECT  key_part_c_key, mes_plan_key , mes_plan_code , product_key, product_code , product_name, p_line_key , p_line_code , p_line_name , wc_key, wc_code , wc_name , part_key , part_code , part_name , quantity FROM  KEY_PART_C ";
                dt = DbHelperSQL.OpenTable(SqlStr);

            }
            catch (Exception ex)
            {
                MessageBox.Show("安全件信息获取失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("安全件信息获取失败！" + ex.Message);
            }
            return dt;
        }
        #endregion

        #region 获取基本信息
        /// <summary>
        /// 根据工位编号，从数据库中连接查询出：公司，工厂，车间，生产线，工位，设备的key,code和name
        /// </summary>
        /// <param name="wc_code">工位编号</param>
        /// <returns></returns>
        public static DataTable GetBasicInfor(string wc_code)
        {
            DataTable dt = new DataTable();
            try
            {
                if (!System.String.IsNullOrEmpty(wc_code) )
                {
                    string SqlStr = "select top 1 WORK_CENTER.wc_key,WORK_CENTER.wc_code,WORK_CENTER.wc_name,EQUIPMENT.equip_key,EQUIPMENT.equip_code,EQUIPMENT.equip_name, P_LINE_WC_LIST.p_line_key,Productionline.p_line_code,Productionline.p_line_name,WS_P_LINE_LIST.ws_key,WORKSHOP.ws_code,WORKSHOP.ws_name,AREA_WS_LIST.area_key,AREA.area_code,AREA.area_name,SITE_AREA_LIST.site_key,SITE.site_code,SITE.site_name  from WORK_CENTER";
                    SqlStr = SqlStr + " join P_LINE_WC_LIST on P_LINE_WC_LIST.wc_key = WORK_CENTER.wc_key";
                    SqlStr = SqlStr + " join Productionline on Productionline.p_line_key = P_LINE_WC_LIST.p_line_key";
                    SqlStr = SqlStr + " join WS_P_LINE_LIST on P_LINE_WC_LIST.p_line_key = WS_P_LINE_LIST.p_line_key";
                    SqlStr = SqlStr + " join WORKSHOP on WORKSHOP.ws_key = WS_P_LINE_LIST.ws_key";
                    SqlStr = SqlStr + " join AREA_WS_LIST on WS_P_LINE_LIST.ws_key = AREA_WS_LIST.ws_key";
                    SqlStr = SqlStr + " join AREA on AREA.area_key = AREA_WS_LIST.area_key";
                    SqlStr = SqlStr + " join SITE_AREA_LIST on SITE_AREA_LIST.area_key = AREA_WS_LIST.area_key";
                    SqlStr = SqlStr + " join SITE on site.site_key = SITE_AREA_LIST.site_key";
                    SqlStr = SqlStr + " join WC_EQUIPMENT_LIST on WC_EQUIPMENT_LIST.wc_key=WORK_CENTER.wc_key";
                    SqlStr = SqlStr + " join EQUIPMENT on EQUIPMENT.equip_key=WC_EQUIPMENT_LIST.equip_key";
                    SqlStr = SqlStr + " where WORK_CENTER.wc_code = '"+ wc_code + "'";
                    dt = DbHelperSQL.OpenTable(SqlStr);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("基本信息获取失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("基本信息获取失败！" + ex.Message);
            }
            return dt;
        }
        #endregion

        #region 获取andon信息
        public static List<AndonInfoDto> GetAndonItemInfor(string stationkey)//获取所有的andon项的信息（上层配置）
        {
            List<AndonInfoDto> andoninfolist = new List<AndonInfoDto>();
            try
            {
                string SqlStr = "select asi.ANDON_INFO_KEY,asi.STATION_KEY,ai.ANDON_INFO_CODE,ai.ANDON_INFO_NAME,ai.ANDON_TYPE_KEY,at.ANDON_TYPE_CODE,at.ANDON_TYPE_NAME from ANDON_STATION_INFO asi";
                SqlStr = SqlStr + " left join ANDON_INFO ai on asi.ANDON_INFO_KEY=ai.ANDON_INFO_KEY";
                SqlStr = SqlStr + " left join ANDON_TYPE at on ai.ANDON_TYPE_KEY= at.ANDON_TYPE_KEY";//从ANDON类型和ANDON项表中获取需要的ANDON信息
                andoninfolist = AndonInfoDtoFactory.Repository().FindListBySql(SqlStr).Where(s=>s.STATION_KEY== stationkey).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ANDON信息获取失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("ANDON信息获取失败！" + ex.Message);
            }
            return andoninfolist;
        }
        public static List<P_ANDON_INFOR> GetAndonInfor(string wc_key)//获取正在执行的andon信息
        {
            List<P_ANDON_INFOR> p_andon_inforlist = new List<P_ANDON_INFOR>();
            try
            {
                if (!System.String.IsNullOrEmpty(wc_key))
                {
                    p_andon_inforlist = P_ANDON_INFORFactory.Repository().FindList("STATION_KEY", wc_key).ToList();
                    return p_andon_inforlist;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("andon呼叫信息获取失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("andon呼叫信息获取失败！" + ex.Message);
            }
            return p_andon_inforlist;
        }
        public static List<P_ANDON_MATERIAL_NEED> GetMaterialAndonInfor(string wc_key)//获取正在执行的andon信息
        {
            List<P_ANDON_MATERIAL_NEED> p_andon_material_need_list = new List<P_ANDON_MATERIAL_NEED>();
            try
            {
                if (!System.String.IsNullOrEmpty(wc_key))
                {
                    p_andon_material_need_list = P_ANDON_MATERIAL_NEEDFactory.Repository().FindList("STATION_KEY", wc_key).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("物料andon详细信息获取失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("物料andon详细信息获取失败！" + ex.Message);
            }
            return p_andon_material_need_list;
        }
        #endregion

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
                MessageBox.Show("零件信息获取失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public static DataTable GetSupplierInfor()
        {
            DataTable dt = new DataTable();
            try
            {                
                string SqlStr = "select supplier_key,supplier_code,supplier_name  from PART_SUPPLIER ";
                dt = DbHelperSQL.OpenTable(SqlStr);
            }
            catch (Exception ex)
            {
                MessageBox.Show("供应商信息获取失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("供应商信息获取失败！" + ex.Message);
            }
            return dt;
        }

        #region 获取订单物料需求数据

        /// <summary>
        /// 查询供应商信息,返回供应商ID，供应商名称，供应商编号
        /// </summary>
        /// <param name="part_code">零部件编号</param>
        /// <returns></returns>
        public static DataTable GetMaterialOrderRequirementInfor(string material_order_requirement_key)
        {
            DataTable dt = new DataTable();
            try
            {
                string SqlStr = @"SELECT  [material_order_requirement_key]
                                         ,[order_code]
                                         ,[product_key]
                                         ,[product_code]
                                         ,[product_name]
                                         ,[product_model_no]
                                         ,[plan_num]
                                         ,[wc_code]
                                         ,[wc_name]
                                         ,[wc_key]
                                         ,[part_key]
                                         ,[part_code]
                                         ,[part_name]
                                         ,[unit]
                                         ,[total_demand_num]
                                         ,[sum_delivery_num]
                                         ,[remark]
                                         ,[CreateDate]
                                         ,[CreateUserId]
                                         ,[CreateUserName]
                                         ,[ModifyDate]
                                         ,[ModifyUserId]
                                         ,[ModifyUserName]
                                         ,[reserve01]
                                         ,[reserve02]
                                         ,[reserve03]
                                         ,[reserve04]
                                         ,[reserve05]
                                      FROM[JAC_FrontAxle].[dbo].[Material_Order_Requirement] where material_order_requirement_key='" + material_order_requirement_key + "'";
                dt = DbHelperSQL.OpenTable(SqlStr);
            }
            catch (Exception ex)
            {
                MessageBox.Show("供应商信息获取失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("供应商信息获取失败！" + ex.Message);
            }
            return dt;
        }
        #endregion
    }    
}
