using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericFW.Areas.Yonetim.GenericVM
{
    public class PageVM
    {
        public int ActivePage { get; set; }
        public int PageCount { get; }
        public int TotalRowCount { get; set; }
        public int PageSize { get; set; }
        public GenericVM SearchVM { get; set; }

    }
}