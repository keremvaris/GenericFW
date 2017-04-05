using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericFW.Attr
{
    public class Column:Attribute
    {
        public Column()
        {
            Visible = false;
            //IsForeign = false;
            Control = ControlEnum.textbox;
        }
        public string DisplayName { get; set; }
        public bool Visible { get; set; }
        public bool Required { get; set; }
        public string Description { get; set; }
        public string ForeignTable { get; set; }
        public string ForeignKey { get; set; }
        public string ForeignValue { get; set; }
        public bool IsForeign { get; set; }

        public bool IsNullable { get; set; }
        public Type InnerType { get; set; }
        public ControlEnum Control { get; set; }

    }
}