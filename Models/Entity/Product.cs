using GenericFW.Attr;
using das = System.ComponentModel.DataAnnotations.Schema;

namespace GenericFW.Models.Entity
{
    [Table(TableName="Products",TableType=TableType.PrimaryTable,
        PrimaryKey="ProductID",IdentityColumn ="ProductID")]
    
    [PageFeatures(Caption ="Ürünler",  Visible =true)]
    public class Product
    {
           
        public int ProductID { get; set; }
        //görünecek mi
        //kolon adı
        [Column(DisplayName="Ürün Adı",Visible=true, Required = true, Control = ControlEnum.textbox)]
        public string ProductName { get; set; }

        [Column(DisplayName = "Kategori ID", Visible = true,  Required = true)]         
        public int? CategoryID { get; set; }

        [das.ForeignKey("CategoryID")]
        public Category Category { get; set; }

        [Column(DisplayName = "Tedarikçi ID", Visible = true)]
        public int? SupplierID { get; set; }
        [das.ForeignKey("SupplierID")]
        public Supplier Supplier { get; set; }

        [Column(DisplayName = "Fiyat", Visible = true)]
        public decimal? UnitPrice { get; set; }     
        [Column(DisplayName = "Stok", Visible = true)]        
        public short?  UnitsInStock { get; set; }
        //[Column(DisplayName = "QuantityPerUnit", Visible = true)]
        public string QuantityPerUnit { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReOrderLevel { get; set; }
        [Column(DisplayName = "Discontinued",Visible =true,Control =ControlEnum.checkbox)]
        public bool Discontinued { get; set; }

    }
}