using CMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericFW.Models.Orm
{
    public class ListDTO
    {
        public dynamic Data { get; set; }
        public Type Type { get; set; }
        public string Title { get; set; }

    }
}