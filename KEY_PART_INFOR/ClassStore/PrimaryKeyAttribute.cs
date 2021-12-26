using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HfutIe
{
    /// <summary>
    /// 主键字段
    /// <author>
    ///		<name>LY</name>
    ///		<date>2017-7-6 17:06:25</date>
    /// </author>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class PrimaryKeyAttribute : Attribute
    {
        public PrimaryKeyAttribute()
        {
        }

        public PrimaryKeyAttribute(string name)
        {
            _name = name;
        }
        private string _name; public virtual string Name { get { return _name; } set { _name = value; } }
    }
}
