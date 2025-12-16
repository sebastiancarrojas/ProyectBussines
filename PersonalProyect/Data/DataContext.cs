using PersonalProyect.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;




namespace PersonalProyect.Data
{
    public class DataContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        // Es el constructor que inicializa tu DataContext y le pasa a EF Core la información
        // para saber cómo conectarse a la base de datos
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        // Estos DbSet representan las tablas en la base de datos
        // public DbSet<Entities.User> Users { get; set; }
        public DbSet<Entities.Customer> Customers { get; set; }
        public DbSet<Entities.Product> Products { get; set; }
        public DbSet<Entities.Sale> Sales { get; set; }
        public DbSet<Entities.SaleDetail> SalesDetails { get; set; }
        public DbSet<Entities.Payment> Payments { get; set; }
        public DbSet<Entities.Permission> Permissions { get; set; }
        public DbSet<Entities.Role> Roles { get; set; }
        public DbSet<Entities.RolePermission> RolePermissions { get; set; }
        public DbSet<Entities.Brand> Brands { get; set; }
        public DbSet<Entities.Category> Categories { get; set; }
        public DbSet<Entities.Supplier> Suppliers { get; set; }
        public DbSet<Entities.ProductSupplier> ProductSuppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ConfigureIndexes(builder);
            ConfigureKeys(modelBuilder);
            base.OnModelCreating(modelBuilder);
            // Aquí puedes configurar las entidades si es necesario
            // Por ejemplo, establecer restricciones adicionales, relaciones, etc.
        }

        private void ConfigureIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<Permission>()
                .HasIndex(p => p.Name)
                .IsUnique();
        }

        private void ConfigureKeys(ModelBuilder modelBuilder)
        {
            // Configuraciones de claves pueden ir aquí
            // Relación Product -> SaleDetails
            modelBuilder.Entity<Product>()
                .HasMany(p => p.SalesDetails)
                .WithOne(sd => sd.Products)
                .HasForeignKey(sd => sd.ProductId);

            // Relación Sale -> SaleDetails
            modelBuilder.Entity<Sale>()
                .HasMany(s => s.SalesDetails)
                .WithOne(sd => sd.Sales)
                .HasForeignKey(sd => sd.SaleId);

            // Relación User -> Sales
            modelBuilder.Entity<User>()
                .HasMany(u => u.Sales)
                .WithOne(s => s.Users)
                .HasForeignKey(s => s.UserId);

            // Relación Brand -> Products
            modelBuilder.Entity<Brand>()
                .HasMany(b => b.Products)
                .WithOne(p => p.Brands)
                .HasForeignKey(p => p.BrandId);

            // Relación Category -> Products
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Categories)
                .HasForeignKey(p => p.CategoryId);

            // Relación Customer -> Sales
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Sales)
                .WithOne(s => s.Customers)
                .HasForeignKey(s => s.CustomerId);

            // Relación Sale -> Payments
            modelBuilder.Entity<Sale>()
                .HasMany(s => s.Payments)
                .WithOne(p => p.Sales)
                .HasForeignKey(p => p.SaleId);

            // Relación RolePermission (PK compuesta)
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.PermissionId, rp.RoleId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            // Relación User -> Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany() // un Role puede tener muchos usuarios
                .HasForeignKey(u => u.ProjectRoleId)
                .OnDelete(DeleteBehavior.Restrict); // evitar borrado en cascada

            modelBuilder.Entity<ProductSupplier>()
                .HasKey(ps => new { ps.ProductId, ps.SupplierId });

            modelBuilder.Entity<ProductSupplier>()
                .HasOne(ps => ps.Product)
                .WithMany(p => p.ProductSuppliers)
                .HasForeignKey(ps => ps.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductSupplier>()
                .HasOne(ps => ps.Supplier)
                .WithMany(s => s.ProductSuppliers)
                .HasForeignKey(ps => ps.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
