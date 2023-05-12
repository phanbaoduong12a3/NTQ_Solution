using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DataLayer.ViewModel
{
    public class ProductModel
    {
        public int ID { get; set; }
        [DisplayName("Tên sản phẩm")]
        [RegularExpression(@"^.{2,150}$", ErrorMessage = "{0} từ 2 đến 150 kí tự")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string ProductName { get; set; }
        public int? CategoryID { get; set; }

        [RegularExpression(@"^.{2,50}$", ErrorMessage = "{0} từ 2 đến 50 kí tự")]
        [Required(ErrorMessage = "Slug không được để trống")]
        public string Slug { get; set; }
        [Column(TypeName = "ntext")]
        [DisplayName("Chi tiết sản phẩm")]
        public string Detail { get; set; }
        [DisplayName("Xu hướng")]
        public bool Trending { get; set; }
        [DisplayName("Trạng thái")]
        public bool Status { get; set; }
        [DisplayName("Lượt xem sản phẩm")]
        public int? NumberViews { get; set; }
        [DisplayName("Giá bán sản phẩm")]
        [Required(ErrorMessage = "Giá bán không được để trống")]
        public double? Price { get; set; }

        [StringLength(150)]
        [DisplayName("Hình ảnh")]
        [Required(ErrorMessage = "Hình ảnh không được để trống")]
        public string Image { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeleteAt { get; set; }
        [Required(ErrorMessage = "Giá nhập không được để trống")]
        [DisplayName("Giá nhập sản phẩm")]
        public double? ImportPrice { get; set; }
        public int? SupplierID { get; set; }
        [DisplayName("Màu sắc sản phẩm")]
        public int? Color { get; set; }
        [DisplayName("Size")]
        public int? Size{get;set;}

        public bool? Sale { get; set; }
        [DisplayName("Số lượng sản phẩm")]
        public int? Count { get; set; }
        [DisplayName("Số lượng nhập")]
        public int ImportCount { get; set; }
        [DisplayName("Số lượng xuất")]
        public int ExportCount { get; set; }
        [DisplayName("Danh mục")]
        public string CategoryName { get; set; }
        [DisplayName("Nhà cung cấp")]
        public string SupplierName { get; set; }


    }
}
