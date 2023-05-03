namespace DataLayer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        public int ID { get; set; }

        public int? UserID { get; set; }

        [Column(TypeName = "ntext")]
        public string Information { get; set; }

        public int? Phone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(150)]
        public string NameStore { get; set; }

        [StringLength(150)]
        public string Address { get; set; }
    }
}
