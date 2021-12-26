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
    /// BasicInfoDto
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.03.21 20:35</date>
    /// </author>
    /// </summary>
    public class AndonInfoDto
    {
        public string STATION_KEY { get; set; }
        public string ANDON_INFO_KEY { get; set; }
        public string ANDON_INFO_CODE { get; set; }
        public string ANDON_INFO_NAME { get; set; }
        public string ANDON_TYPE_KEY { get; set; }
        public string ANDON_TYPE_CODE { get; set; }
        public string ANDON_TYPE_NAME { get; set; }
    }
}