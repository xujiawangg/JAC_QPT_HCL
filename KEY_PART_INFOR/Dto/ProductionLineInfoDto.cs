//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2017
// Software Developers @ HfutIE 2017
//=====================================================================================

using System;
using System.ComponentModel;
using System.Text;

namespace KEY_PART_INFOR
{
    /// <summary>
    /// ProductionLineInfoDto
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.03.21 20:35</date>
    /// </author>
    /// </summary>
    public class ProductionLineInfoDto
    {
        public string PRODUCTION_LINE_KEY { get; set; }
        public string PRODUCTION_LINE_CODE { get; set; }
        public string PRODUCTION_LINE_NAME { get; set; }
        public string WORK_CENTER_KEY { get; set; }
        public string WORK_CENTER_CODE { get; set; }
        public string WORK_CENTER_NAME { get; set; }
        public string STATION_KEY { get; set; }
        public string STATION_CODE { get; set; }
        public string STATION_NAME { get; set; }
    }
}