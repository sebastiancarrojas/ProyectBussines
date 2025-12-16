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
                new Product
                {
                    Id = Guid.NewGuid(),
                    ProductName = "CALDO DE GALLINA MAGGI 10 UND 110 G",
                    ProductDescription = "Disfruta del auténtico sabor del caldo de gallina Maggi." +
                                         "Perfecto para añadir un toque especial a tus recetas favoritas. Este caldo" +
                                         "es ideal como sazonador, condimento o saborizante para diferentes preparaciones culinarias.",
                    UnitPrice = 3990,
                    StockMin = 10,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = "Activo",
                    CategoryId = alimentosId,
                    BrandId = maggiId
                },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "ARROZ DIANA 5KG",
                ProductDescription = "Arroz Diana de alta calidad, grano entero y blanco. Perfecto para acompañar tus comidas diarias.",
                Barcode = "7701234567001",
                UnitPrice = 18900,
                StockMin = 15,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "CAFÉ JUAN VALDÉZ 500G",
                ProductDescription = "Café 100% colombiano, tostado y molido. Aroma y sautentico de las mejores regiones cafeteras.",
                Barcode = "7701234567002",
                UnitPrice = 24500,
                StockMin = 12,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "GASEOSA COCA COLA 2.5L",
                ProductDescription = "Refresco de cola familiar para compartir en cualquier ocasión.",
                Barcode = "7701234567003",
                UnitPrice = 8900,
                StockMin = 20,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "PANELA RAPADURA 1KG",
                ProductDescription = "Panela orgánica, endulzante natural rico en minerales y vitaminas.",
                Barcode = "7701234567004",
                UnitPrice = 6500,
                StockMin = 25,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "QUESO COSTEÑO LA SERENA 500G",
                ProductDescription = "Queso costeño tradicional, ideal para desayunos y meriendas.",
                Barcode = "7701234567005",
                UnitPrice = 12500,
                StockMin = 10,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "CHICHARRÓN EL RANCHE 250G",
                ProductDescription = "Chicharrón crujiente tradicional, perfecto para acompañar con yuca o arepa.",
                Barcode = "7701234567006",
                UnitPrice = 8500,
                StockMin = 18,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "ACEITE GOURMET 1L",
                ProductDescription = "Aceite vegetal 100% puro, ideal para freír y cocinar.",
                Barcode = "7701234567007",
                UnitPrice = 15800,
                StockMin = 15,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "HUEVOS AA X 30 UND",
                ProductDescription = "Huevos tipo AA frescos, de gran tamaño y calidad superior.",
                Barcode = "7701234567008",
                UnitPrice = 22500,
                StockMin = 12,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "LECHE ALQUERÍA 1.1L",
                ProductDescription = "Leche entera pasteurizada, fortificada con vitaminas A y D.",
                Barcode = "7701234567009",
                UnitPrice = 6500,
                StockMin = 25,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "YOGURT ALPINA 150G",
                ProductDescription = "Yogurt griego con frutas, alto en proteína y bajo en grasa.",
                Barcode = "7701234567010",
                UnitPrice = 3200,
                StockMin = 30,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "SALCHICHÓN ZENÚ 500G",
                ProductDescription = "Salchichón ahumado tradicional, perfecto para desayunos y picadas.",
                Barcode = "7701234567011",
                UnitPrice = 14500,
                StockMin = 10,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "MORTADELA LA ESPECIAL 400G",
                ProductDescription = "Mortadela fina con pimienta en grano, corte deli.",
                Barcode = "7701234567012",
                UnitPrice = 9800,
                StockMin = 15,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "AREQUIPE ALPINA 250G",
                ProductDescription = "Dulce de leche cremoso, ideal para postres y acompañamientos.",
                Barcode = "7701234567013",
                UnitPrice = 7500,
                StockMin = 20,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "GALLETAS FESTIVAL 500G",
                ProductDescription = "Galletas con crema de chocolate, clásicas y deliciosas.",
                Barcode = "7701234567014",
                UnitPrice = 6800,
                StockMin = 25,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "PASTA DORIA 500G",
                ProductDescription = "Pasta de sémola de trigo dura, espagueti número 5.",
                Barcode = "7701234567015",
                UnitPrice = 4200,
                StockMin = 30,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "ATÚN VAN CAMPS 180G",
                ProductDescription = "Atún en trozos en aceite vegetal, alto en proteína.",
                Barcode = "7701234567016",
                UnitPrice = 8500,
                StockMin = 20,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "SARDINAS LA GAVIOTA 425G",
                ProductDescription = "Sardinas en salsa de tomate, fuente de omega 3.",
                Barcode = "7701234567017",
                UnitPrice = 7200,
                StockMin = 18,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "JABÓN REY 3 UND",
                ProductDescription = "Jabón de barra antibacterial, aroma tradicional.",
                Barcode = "7701234567018",
                UnitPrice = 9500,
                StockMin = 25,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "PAPEL HIGIÉNICO FAMILIA 12 ROLLOS",
                ProductDescription = "Papel higiénico suave y resistente, doble hoja.",
                Barcode = "7701234567019",
                UnitPrice = 28500,
                StockMin = 10,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "DETERGENTE LAUNDRY 3KG",
                ProductDescription = "Detergente en polvo para ropa blanca y de color.",
                Barcode = "7701234567020",
                UnitPrice = 32500,
                StockMin = 8,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "PAPA PASTUSA 5KG",
                ProductDescription = "Papa pastusa para cocinar, fritar o preparar puré.",
                Barcode = "7701234567021",
                UnitPrice = 16500,
                StockMin = 20,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "TOMATE CHONTO 1KG",
                ProductDescription = "Tomate chonto fresco, ideal para salsas y ensaladas.",
                Barcode = "7701234567022",
                UnitPrice = 5800,
                StockMin = 30,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "CEBOLLA CABEZONA 1KG",
                ProductDescription = "Cebolla cabezona blanca, para todo tipo de preparaciones.",
                Barcode = "7701234567023",
                UnitPrice = 4500,
                StockMin = 35,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "PLÁTANO MADURO 1KG",
                ProductDescription = "Plátano maduro ideal para tajadas, patacones o asado.",
                Barcode = "7701234567024",
                UnitPrice = 5200,
                StockMin = 25,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
            },
            new Product
            {
                Id = Guid.NewGuid(),
                ProductName = "AGUACATE HASS 1KG",
                ProductDescription = "Aguacate hass maduro, perfecto para guacamole o ensaladas.",
                Barcode = "7701234567025",
                UnitPrice = 18500,
                StockMin = 15,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = "Activo",
                CategoryId = CategoryNullId,
                BrandId = BrandNullId
                }

            };
        }
    }
}
