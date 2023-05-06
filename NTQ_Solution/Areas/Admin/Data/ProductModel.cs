using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NTQ_Solution.Areas.Admin.Data
{
    public class ProductModel
    {
        public int ID { get; set; }

        [RegularExpression(@"^.{2,150}$", ErrorMessage = "{0} từ 2 đến 150 kí tự")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string ProductName { get; set; }
        public int? CategoryID { get; set; }

        [RegularExpression(@"^.{2,150}$", ErrorMessage = "{0} e đến 150 kí tự")]
        [Required(ErrorMessage = "Slug không được để trống")]
        public string Slug { get; set; }
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public bool Trending { get; set; }

        public bool Status { get; set; }

        public int? NumberViews { get; set; }
        [Required(ErrorMessage = "Giá xuất không được để trống")]
        public double? Price { get; set; }

        [StringLength(150)]
        [Required(ErrorMessage = "Select Image,please")]
        public string Image { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeleteAt { get; set; }
        [Required(ErrorMessage = "Giá nhập không được để trống")]
        public double? ImportPrice { get; set; }
        public int? SupplierID { get; set; }
        [StringLength(50)]
        public string Color { get; set; }

        [StringLength(50)]
        public string Size { get; set; }

        public bool? Sale { get; set; }

        public int? Count { get; set; }
        public int ImportCount { get; set; }
        public int ExportCount { get; set; }
    }
}