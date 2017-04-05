using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;  
using GenericFW.Attr;
using das = System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GenericFW.Models.Entity
{
    [Table(TableType = TableType.PrimaryTable, TableName = "Categories", PrimaryKey = "CategoryID", IdentityColumn = "CategoryID")]
    [PageFeatures(Caption = "Kategoriler", Visible = true)]
    [das.DisplayColumn("CategoryName")]
    public class Category
    {
        [das.Key]
        public int CategoryID { get; set; }
        [Column(DisplayName = "Kategori Adı", Visible = true)]
        public string CategoryName { get; set; }
        [Column(DisplayName = "Açıklama", Visible = true)]
        public string Description { get; set; }
    }
}