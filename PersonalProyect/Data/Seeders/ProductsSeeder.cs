using Microsoft.EntityFrameworkCore;
using PersonalProyect.Data;
using PersonalProyect.Data.Entities;
using PersonalProyect.Data.Seeders;
namespace PersonalProyect.Data.Seeders
{
    public class ProductSeeder
    {
        // -------------------------------
        // -- Inyección de dependencias --
        // -------------------------------

        private readonly DataContext _context;

        public ProductSeeder(DataContext context)
        {
            _context = context;
        }

        // -----------------------
        // -- Método de siembra --
        // -----------------------
        public async Task SeedAsync()
        {
            var bebidasId = await SeederHelper.GetCategoryId(_context, "Bebidas");
            var alimentosId = await SeederHelper.GetCategoryId(_context, "Alimentos");
            var CategoryNullId = await SeederHelper.GetCategoryId(_context, "Ninguno");

            var coronaId = await SeederHelper.GetBrandId(_context, "Corona");
            var lattiId = await SeederHelper.GetBrandId(_context, "LATTI");
            var maggiId = await SeederHelper.GetBrandId(_context, "MAGGI");
            var BrandNullId = await SeederHelper.GetBrandId(_context, "Ninguno");

            var products = Products(bebidasId, alimentosId, coronaId, lattiId, maggiId, CategoryNullId, BrandNullId);

            foreach (var product in products)
            {
                bool exists = await _context.Products.AnyAsync(p => p.ProductName == product.ProductName);
                if (!exists)
                {
                    await _context.Products.AddAsync(product);
                }
            }

            await _context.SaveChangesAsync();
        }

        // ------------------------------
        // ----- Lista de productos -----
        // ------------------------------

        private List<Product> Products(Guid bebidasId, Guid alimentosId, Guid coronaId, Guid lattiId, Guid maggiId, Guid CategoryNullId, Guid BrandNullId)
        {
            return new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "CERVEZA CORONITA 210 ML",
                    ProductDescription = "La Cerveza Coronita es una bebida alcohólica de gran calidad, conocida" +
                                         "en todo el mundo por su sabor fresco con un toque de amargor. Viene en una" +
                                         "botella de vidrio de 210 ml, perfecta para disfrutar bien fría en cualquier momento.",
                    UnitPrice = 2900,
                    StockMin = 5,
                    UnitOfMeasure = "ml",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = "Activo",
                    CategoryId = bebidasId,
                    BrandId = coronaId
                },

                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "LECHE EQUILIBRIO TETRAPAK UHTLATTI 900ML",
                    ProductDescription = "La Leche Equilibrio UHT de Latti, en envase Tetra Pak de" +
                                         "900 mL, es una leche semidescremada de larga duración. Su presentación" +
                                         "práctica y resistente la hace perfecta para el consumo diario.",
                    UnitPrice = 3250,
                    StockMin = 2,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = "Activo",
                    CategoryId = alimentosId,
                    BrandId = lattiId
                },
            };
        }
    }
}
