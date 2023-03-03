namespace DataLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public int ID { get; set; }

        [StringLength(50)]
        [DisplayName("Username")]
        public string UserName { get; set; }

        [StringLength(50)]

        [DisplayName("PassWord")]
        public string PassWord { get; set; }

        public int? Role { get; set; }

        [StringLength(50)]

        [DisplayName("Email")]
        public string Email { get; set; }

        public int? Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Create_at { get; set; }
        [DisplayFormat(DataFormatString = "{dd/mm/yyyy}")]

        public DateTime? Update_at { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Delete_at { get; set; }
    }
}
