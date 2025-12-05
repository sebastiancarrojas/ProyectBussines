using PersonalProyect.Data.Entities;
using Microsoft.EntityFrameworkCore;



namespace PersonalProyect.Data
{
    public class DataContext : DbContext
    {
        // Es el constructor que inicializa tu DataContext y le pasa a EF Core la información
        // para saber cómo conectarse a la base de datos
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        // Estos DbSet representan las tablas en la base de datos
        public DbSet<Entities.Administrator> Administrators { get; set; }
        public DbSet<Entities.Customer> Customers { get; set; }
        public DbSet<Entities.Product> Products { get; set; }
        public DbSet<Entities.Sale> Sales { get; set; }
        public DbSet<Entities.SaleDetail> SalesDetails { get; set; }
        public DbSet<Entities.Payment> Payments { get; set; }

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
            // Configuraciones de índices pueden ir aquí
        }

        private void ConfigureKeys(ModelBuilder modelBuilder)
        {
            // Configuraciones de claves pueden ir aquí
            modelBuilder.Entity<Product>().HasMany(b => b.SalesDetails).WithOne(p => p.Products).HasForeignKey(b => b.ProductId);
            modelBuilder.Entity<Sale>().HasMany(b => b.SalesDetails).WithOne(p => p.Sales).HasForeignKey(b => b.SaleId);
            modelBuilder.Entity<Administrator>().HasMany(b => b.Sales).WithOne(p => p.Administrators).HasForeignKey(b => b.AdministratorId);
            modelBuilder.Entity<Customer>().HasMany(b => b.Sales).WithOne(p => p.Customers).HasForeignKey(b => b.CustomerId);
            modelBuilder.Entity<Sale>().HasMany(b => b.Payments).WithOne(p => p.Sales).HasForeignKey(b => b.SaleId);
        }
    }
}
