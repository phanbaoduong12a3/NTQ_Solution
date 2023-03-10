using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTQ_Solution.Areas.Admin.Data
{
    public class ProductModel
    {
        public int ID { get; set; }

        [RegularExpression(@"^.{2,50}$", ErrorMessage = "{0} from 2 to 50 characters")]
        [Required(ErrorMessage = "Enter ProductName,please")]
        public string ProductName { get; set; }
        public int? CategoryID { get; set; }

        [RegularExpression(@"^.{2,150}$", ErrorMessage = "{0} from 2 to 150 characters")]
        [Required(ErrorMessage = "Enter Slug,please")]
        public string Slug { get; set; }

        [StringLength(50)]
        public string Detail { get; set; }

        public bool Trending { get; set; }

        public bool Status { get; set; }

        public int? NumberViews { get; set; }
        [Required(ErrorMessage = "Enter Price,please")]
        public double? Price { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Select Image,please")]
        public string Image { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeleteAt { get; set; }

    }
}