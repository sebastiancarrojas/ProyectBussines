using PersonalProyect.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace PersonalProyect.Data.Seeders
{
    public class BrandsSeeder
    {
        // -------------------------------
        // -- Inyección de dependencias --
        // -------------------------------

        private readonly DataContext _context;
        public BrandsSeeder(DataContext context)
        {
            _context = context;
        }

        // -----------------------
        // -- Método de siembra --
        // -----------------------

        public async Task SeedAsync()
        {
            List<Brand> brands = Brands();
            foreach (Brand brand in brands)
            {
                bool exists = await _context.Brands.AnyAsync(b => b.BrandName == brand.BrandName);
                if (!exists)
                {
                    await _context.Brands.AddAsync(brand);
                }
            }
            await _context.SaveChangesAsync();
        }

        // ------------------------------
        // ------ Lista de marcas -------
        // ------------------------------

        private List<Brand> Brands()
        {
            return new List<Brand>
            {
                new Brand
                {
                    Id = Guid.NewGuid(),
                    BrandName = "Corona",
                    BrandDescription = "Coronita pertenece a la reconocida familia de cervezas Corona," +
                                       "una marca con casi un siglo de tradición cervecera. Elaborada con" +
                                       "una combinación equilibrada de malta de cebada, maíz y lúpulo, destaca" +
                                       "por su sabor ligero y refrescante. Con presencia internacional y una" +
                                       "sólida reputación en diversos mercados, Coronita representa la calidad" +
                                       "y el estilo característico que han convertido a la marca en una de las" +
                                       "favoritas a nivel global.",
                    Status = "Active"
                },
                new Brand
                {
                    Id = Guid.NewGuid(),
                    BrandName = "LATTI",
                    BrandDescription = "Latti es una marca dedicada a ofrecer productos lácteos de calidad, elaborados" +
                                        "bajo altos estándares para garantizar frescura, buen sabor y nutrición. Reconocida por su" +
                                        "compromiso con la innovación y el bienestar familiar, Latti destaca por brindar opciones" +
                                        "prácticas y confiables para el consumo diario, adaptándose a las necesidades de cada hogar.",
                    Status = "Active"
                },
                new Brand
                {
                    Id = Guid.NewGuid(),
                    BrandName = "MAGGI",
                    BrandDescription = "MAGGI es una marca reconocida a nivel mundial por su amplia variedad de productos culinarios " +
                                        "que facilitan la preparación de comidas prácticas y llenas de sabor. Con décadas de presencia en los hogares, " +
                                        "MAGGI se distingue por ofrecer caldos, sopas, salsas y sazonadores elaborados con ingredientes de calidad, pensados " +
                                        "para realzar cada receta y hacer la cocina diaria más sencilla y deliciosa.",
                    Status = "Active"
                },
                new Brand
                {
                    Id = Guid.NewGuid(),
                    BrandName = "Ninguno",
                    BrandDescription = "Ninguno",
                    Status = "Active"
                },
            };
        }
    }
}
