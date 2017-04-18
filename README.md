# GenericFW
Belli bir notasyonda yazılan modeli reflection ve metadata kullanarak CRUD işlemlerin yapılmasına hazır hale getiren bir Asp.Net MVC Projesidir.

<img src="http://keremvaris.com/image/fw.png" width="800" height="600"/>
Web sitelerinin Yönetici panelleri ve / veya orta düzeyde takip
yazılımları geliştirmede faydalı olabilecek bir micro orm denilebilir. Default
olarak SQLite desteğiyle gelir Entity Frameworkün desteklediği tüm
veritabanlarıyla uyum sağlanabilir. Expando Object , Reflection,
MetaData'nın ne kadar güçlü olabileceğini işleri ne kadar
basitleştireceğini göstermek için yapılmıştır. Entity Framework Annotationlarına 
sadık kalındığı gibi istenirse yeni bir annotation oluşturulabilir.

GenericController içinde bulunan tüm CRUD metodları virtual olduğundan override edilebilir 
özel ekran yapacağım entity framework kullanacağım elimle kendim yazacağım derseniz sizi engellemez.

Şimdilik iki Tip Lookup Çözer bir tanesi düz lookup (bkz Role) diğer Enum Lookup (bkz Gender) kullanımları User Classında görülebilir.


Kısacası Projenin ayağa kaldırılması aşamasına gelince Code First yapısında ;

1. GenericFW.Entities projesinde Class veya Class'lar oluşturulur.
2. GenericFW Projesinde Areas -> Yonetim -> Controllers altında aşağıdaki gibi  boş bir Controller oluşturulur.


    public class UserController : GenericController<User>
    {        
    }


3. Package Manager Console açılır GenericFW.DataAccessLayer projesi seçilir
4. Add-Migration Startup yazılır Enter'a basılır.
5. Update-Database yazılır Enter'a Basılır. 
6. F5 Tuşuyla Proje çalıştırılır.
7. http://localhost:<PORT_>/Yonetim/User Örnek Proje için geçerlidir. 

Yonetim alanından sonra Oluşturacağınız Class ismine ne verdiyseniz onu yazıp Enter'a basmanız yeterlidir.

Basit örnek Class GenericFW.Entities Projeninin altında User.cs içindedir


    [Table("Users")]
    [DisplayColumn("Name")]
    [DisplayName("Kullanıcılar")]
    public class User : EntityBase
    {
        [Display(Name = "Ad"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        [Display(Name = "Surname")]
        [DataType(DataType.Text)]
        public string Surname { get; set; }

        [Display(Name = "Cinsiyet")]
        public Gender Gender { get; set; }

        [Display(Name = "Rolü")]
        [Required]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        [Display(Name = "Aktif mi?")]
        public bool Active { get; set; }

        [ScaffoldColumn(false), Required]
        [Display(Name="Açıklama")]
        [DataType(DataType.Html)]

        public string Aciklama { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Resim Yükle")]
        [DataType(DataType.Upload)]

        public string UserImage { get; set; }
    }

    public enum Gender
    {
        [Display(Name = "Erkek")]
        Male = 1,
        [Display(Name = "Kadın")]
        Female = 2
    }

    [Table("Roles")]
    [DisplayColumn("Name")]
    [PageFeatures(Caption = "Hello")]
    [DisplayName("Roller")]
    public class Role : EntityBase
    {
        [Display(Name = "Ad")]
        [MaxLength(50)]
        public string Name { get; set; }

    }
