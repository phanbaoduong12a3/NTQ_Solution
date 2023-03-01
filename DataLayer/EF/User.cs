namespace DataLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string PassWord { get; set; }

        public int? Role { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public int? Status { get; set; }

        public DateTime? Create_at { get; set; }

        public DateTime? Update_at { get; set; }

        public DateTime? Delete_at { get; set; }
    }
}
