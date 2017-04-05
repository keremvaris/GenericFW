using GenericFW.Attr;
using das = System.ComponentModel.DataAnnotations;

namespace GenericFW.Models.Entity
{
    [Table(TableType = TableType.PrimaryTable, TableName = "Reviews", PrimaryKey = "ReviewsId", IdentityColumn = "ReviewsId")]
    [PageFeatures(Caption = "Reviews", Visible = true)]
    [das.DisplayColumn("TRCaption")]
    public class Reviews
    {
        [das.Key]
        public int ReviewsId { get; set; }
        [Column(DisplayName = "Kategori Adı", Visible = true)]
        public string TRCaption { get; set; }
        [Column(DisplayName = "Açıklama", Visible = true)]
        public string TRContent { get; set; }
    }
}