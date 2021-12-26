using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HfutIe
{
    class CreateAGVForm
    {
        static Dictionary<string,AgvForm> has_show_part = new Dictionary<string, AgvForm>();
        public static AgvForm GetAGVForm(string wc_code, string part_code,out bool is_exist)
        {
            AgvForm agvform;
            is_exist = has_show_part.TryGetValue(part_code, out agvform);
            if (!is_exist)
            {
                agvform = new HfutIe.AgvForm(wc_code, part_code);
                agvform.FormClosed += new FormClosedEventHandler((sender,e)=> has_show_part.Remove(part_code));//从字典中移除
                has_show_part.Add(part_code, agvform);
            }
            return agvform;
        }
    }
}
