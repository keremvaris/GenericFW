using GenericFW.Attr;
using System;

namespace GenericFW.Models.Entity
{
    [Table(TableType = TableType.PrimaryTable, TableName = "Employees", PrimaryKey = "EmployeeID", IdentityColumn = "EmployeeID")]
    [PageFeatures(Caption = "Çalışanlar", Visible = true)]
    public class Employee
    {
        public int EmployeeID { get; set; }
        [Column(DisplayName="Soy Adı",Visible=true)]
        public string LastName { get; set; }
        [Column(DisplayName = "Adı", Visible = true)]
        public string FirstName { get; set; }
        [Column(DisplayName = "Ünvanı", Visible = true)]
        public string Title { get; set; }
       
        public string TitleOfCourtesy { get; set; }
        [Column(DisplayName = "Doğum Tarihi", Visible = true)]
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
       
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        [Column(Control =ControlEnum.ckeditor, DisplayName = "Notlar", Visible = true)]
        public string Notes { get; set; }

        [Column(DisplayName ="Raporlanan",Visible =true)]         
        public int? ReportsTo { get; set; }

      
        public string PhotoPath { get; set; }

    }
    
}