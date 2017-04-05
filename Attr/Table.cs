using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace GenericFW.Attr
{


    public class Table : Attribute
    {
        public Table()
        {
            this.SchemaName = "dbo";
            this.TableName = "sys.Tables";
            this.PrimaryKey = "Id";
            this.IdentityColumn = "Id";
            this.TableType = TableType.PrimaryTable;
            this.ConnectionName = "DataBase";
            this.SearchFields = new string[] { "Name" };
            this.CacheState = CacheType.NoCache;
        }

        public int BETypeID { get; set; }

        public CacheType CacheState { get; set; }

        public string[] CompositeKeys { get; set; }

        public string ConnectionName { get; set; }

        public string IdentityColumn { get; set; }

        public bool IsActiveValid { get; set; }

        public string PrimaryKey { get; set; }

        public string SchemaName { get; set; }

        public string[] SearchFields { get; set; }

        public string TableName { get; set; }

        public TableType TableType { get; set; }
    }
}