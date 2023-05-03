using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModel
{
    public class ReviewModel
    {
        public int ID { get; set; }

        public int? ProductsID { get; set; }

        public int? UserID { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public string UserName { get; set; }

        public string Image { get; set; }

        public double? View { get; set; }

        public string ProductName { get; set; }
        public int? Status { get; set; }
        public int? ParentID { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeleteAt { get; set; }
    }
}
