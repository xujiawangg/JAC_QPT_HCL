using HfutIe;
using HfutIE.Entity;
using HfutIE.Repository;
using HfutIE.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HfutIe
{
    public class ServerDictionary
    {
        #region 仓储        
        static RepositoryFactory<Base_DataDictionary> Base_DataRepository = new RepositoryFactory<Base_DataDictionary>();
        static RepositoryFactory<Base_DataDictionaryDetail> Base_DataDetailRepository = new RepositoryFactory<Base_DataDictionaryDetail>();
        #endregion

        /// <summary>
        /// 开发者名称
        /// </summary>
        private static string laboratoryname;
        /// <summary>
        /// 程序中文名称
        /// </summary>
        private static string programname_cn_cs;
        /// <summary>
        /// 程序英文名称
        /// </summary>
        private static string programname_cn_en;
        /// <summary>
        /// 服务器端口号
        /// </summary>
        private static string ipcode;
        /// <summary>
        /// 底层定时刷新时间
        /// </summary>
        private static string timervalue;

        /// <summary>
        /// 静态构造函数，通过数据字典获取程序名称，数据字典信息
        /// </summary>
        static ServerDictionary()
        {
            List<Base_DataDictionary> alldiclist = Base_DataRepository.Repository().FindList();//获取所有数据字典信息

            #region 取主界面所有LabelName值
            Base_DataDictionary L_Info = alldiclist.Find(s => s.FullName == "LableName");
            List<Base_DataDictionaryDetail> L_Info_De = Base_DataDetailRepository.Repository().FindList("DATADICTIONARYID", L_Info.DataDictionaryId);
            foreach (var item_lable in L_Info_De)
            {
                if (item_lable.FullName == "LaboratoryName")
                {
                    laboratoryname = item_lable.Code;
                }
                if (item_lable.FullName == "ProgramName_CN_CS")
                {
                    programname_cn_cs = item_lable.Code;
                }
                if (item_lable.FullName == "ProgramName_CN_EN")
                {
                    programname_cn_en = item_lable.Code;
                }
            }
            #endregion

            #region 取服务器IP端口号
            Base_DataDictionary IP_Info = alldiclist.Find(s => s.FullName == "服务器IP端口");
            Base_DataDictionaryDetail IP_Info_De = Base_DataDetailRepository.Repository().FindList().Where(s => s.DataDictionaryId == IP_Info.DataDictionaryId && s.FullName == "主IP端口").FirstOrDefault();
            ipcode = IP_Info_De.Code;//获取服务器IP端口号存入静态变量
            #endregion

            #region 获取底层定时刷新时间
            string vaule = "底层定时刷新";
            Base_DataDictionary Address_Type = Base_DataRepository.Repository().FindEntity("FULLNAME", vaule);
            Base_DataDictionaryDetail time_detail = Base_DataDetailRepository.Repository().FindEntity("DATADICTIONARYID", Address_Type.DataDictionaryId);
            timervalue = time_detail.Code;
            #endregion
        }

        /// <summary>
        /// 获取开发者名称
        /// </summary>
        public static string LaboratoryName
        {
            get
            {
                return laboratoryname;
            }
        }
        /// <summary>
        /// 获取程序中文名称
        /// </summary>
        public static string ProgramName_CN_CS
        {
            get
            {
                return programname_cn_cs;
            }
        }
        /// <summary>
        /// 获取程序英文名称
        /// </summary>
        public static string ProgramName_CN_EN
        {
            get
            {
                return programname_cn_en;
            }
        }
        /// <summary>
        /// 获取服务器端口号
        /// </summary>
        public static string IPCode
        {
            get
            {
                return ipcode;
            }
        }
        /// <summary>
        /// 获取底层定时刷新时间
        /// </summary>
        public static string timeValue
        {
            get
            {
                return timervalue;
            }
        }
    }
}