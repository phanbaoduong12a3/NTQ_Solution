using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DataLayer.EF
{
    public partial class NTQDBContext : DbContext
    {
        public NTQDBContext()
            : base("name=NTQDBContext1")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<Export> Exports { get; set; }
        public virtual DbSet<Import> Imports { get; set; }
        public virtual DbSet<Media> Medias { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Shipping> Shippings { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Color> Colors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
