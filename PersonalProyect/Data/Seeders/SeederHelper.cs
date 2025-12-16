using Microsoft.EntityFrameworkCore;

namespace PersonalProyect.Data.Seeders
{
    public static class SeederHelper
    {
        public static async Task<Guid> GetCategoryId(DataContext context, string name)
        {
            var entity = await context.Categories.FirstOrDefaultAsync(c => c.CategoryName == name);
            if (entity == null)
                throw new Exception($"La categoría '{name}' no existe en la BD.");

            return entity.Id;
        }

        public static async Task<Guid> GetBrandId(DataContext context, string name)
        {
            var entity = await context.Brands.FirstOrDefaultAsync(b => b.BrandName == name);
            if (entity == null)
                throw new Exception($"La marca '{name}' no existe en la BD.");

            return entity.Id;
        }
    }
}
