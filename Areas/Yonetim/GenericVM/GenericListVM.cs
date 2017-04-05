using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericFW.Areas.Yonetim.GenericVM
{
    public class GenericListVM
    {
        public TableMeta Meta { get; private set; }
        public PageVM Page { get; set; }

        public GenericListVM(TableMeta meta)
        {
            this.Meta = meta;
        }

        public List<IDictionary<string, object>> Items { get; set; }
    }
}