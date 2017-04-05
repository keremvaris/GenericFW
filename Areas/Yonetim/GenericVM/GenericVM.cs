using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericFW.Areas.Yonetim.GenericVM
{
    public class GenericVM
    {
        public TableMeta Meta { get; private set; }

        public GenericVM(TableMeta meta)
        {
            this.Meta = meta;
        }

        public IDictionary<string, object> Data { get; set; }

    }
}