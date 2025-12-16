using PersonalProyect.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace PersonalProyect.Data.Seeders
{
    public class CategoriesSeeder
    {
        // -------------------------------
        // -- Inyección de dependencias --
        // -------------------------------

        private readonly DataContext _context;
        public CategoriesSeeder(DataContext context)
        {
            _context = context;
        }

        // -----------------------
        // -- Método de siembra --
        // -----------------------

        public async Task SeedAsync()
        {
            List<Category> categories = Categories();
            foreach (Category category in categories)
            {
                bool exists = await _context.Categories.AnyAsync(c => c.CategoryName == category.CategoryName);
                if (!exists)
                {
                    await _context.Categories.AddAsync(category);
                }
            }
            await _context.SaveChangesAsync();
        }

        // ------------------------------
        // ---- Lista de categorías -----
        // ------------------------------

        private List<Category> Categories()
        {
            return new List<Category>
            {
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Bebidas",
                    CategoryDescription = "Productos líquidos destinados al consumo humano, incluyendo refrescos, jugos, aguas, cervezas y otras bebidas alcohólicas.",
                    Status = "Active"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Alimentos",
                    CategoryDescription = "Productos comestibles que incluyen alimentos frescos, procesados y envasados, abarcando una amplia variedad de opciones para la nutrición humana.",
                    Status = "Active"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Ninguno",
                    CategoryDescription = "Ninguno",
                    Status = "Active"
                }
            };
        }
    }
}
