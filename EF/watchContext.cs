using aspcore_watchshop.Entities;
using Microsoft.EntityFrameworkCore;

namespace aspcore_watchshop.EF {
    public class watchContext : DbContext {
        public watchContext (DbContextOptions<watchContext> options) : base (options) { }

        //DbSets    
        public DbSet<Band> Bands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Info> Infos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromProduct> PromProducts { get; set; }
        public DbSet<PromBill> PromBills { get; set; }
        public DbSet<TypeWire> TypeWires { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            base.OnModelCreating (modelBuilder);
            //Config OrderDetail
            modelBuilder.Entity<OrderDetail> ().HasKey (od => new { od.OrderID, od.ProductID });
            //Config Order
            modelBuilder.Entity<Order> ().Property (o => o.Status).HasDefaultValue (1);
            modelBuilder.Entity<Order> ().Property (o => o.DateCreated).HasColumnType ("smalldatetime");
            //Config Promotion
            modelBuilder.Entity<Promotion> ().Property (p => p.Status).HasDefaultValue (true);
            modelBuilder.Entity<Promotion> ().Property (p => p.ToDate).HasColumnType ("smalldatetime");
            modelBuilder.Entity<Promotion> ().Property (p => p.FromDate).HasColumnType ("smalldatetime");
            modelBuilder.Entity<PromBill> ().Property (pb => pb.isFeeShip).HasDefaultValue (false);
            //Config Product
            modelBuilder.Entity<Product> ().Property (p => p.isShow).HasDefaultValue (true);
            modelBuilder.Entity<Product> ().Property (p => p.isDel).HasDefaultValue (false);
            modelBuilder.Entity<Product> ().Property (p => p.SaleCount).HasDefaultValue (0);
            //Config Info
            modelBuilder.Entity<Info> ().HasNoKey ();
        }

    }
}