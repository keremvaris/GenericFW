using GenericFW.Attr;
using das = System.ComponentModel.DataAnnotations;

namespace GenericFW.Models.Entity
{
    [Table(TableType = TableType.PrimaryTable, TableName = "Suppliers", PrimaryKey = "SupplierID", IdentityColumn = "SupplierID")]
    [PageFeatures(Caption = "Tedarikçiler", Visible = true)]
    [das.DisplayColumn("CompanyName")]
    public class Supplier
    {
        [das.Key]
        public int SupplierID { get; set; }
        [Column(DisplayName = "Tedarikçi Adı", Visible = true)]
        public string CompanyName { get; set; }           
    }
}