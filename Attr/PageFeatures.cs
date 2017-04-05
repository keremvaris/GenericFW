//using CMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericFW.Attr
{
    public class PageFeatures:Attribute
    {
        public PageFeatures()
        {
            Caption = "";
            Visible = false;
        }
        public string Caption { get; set; }
        public bool Visible { get; set; }

    }
}