namespace DataLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [DisplayName("Tên danh mục")]
        public string CategoryName { get; set; }
        [DisplayName("Trạng thái")]
        public bool Status { get; set; }
    }
}
