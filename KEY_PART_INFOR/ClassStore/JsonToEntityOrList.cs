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
    public class JsonToEntityOrList
    {
        
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
        static JsonToEntityOrList()
        {
            
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