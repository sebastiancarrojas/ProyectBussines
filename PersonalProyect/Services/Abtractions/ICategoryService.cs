using PersonalProyect.Core;
using PersonalProyect.DTOs.Categories;

namespace PersonalProyect.Services.Abtractions
{
    public interface ICategoryService
    {
        public Task<Response<CategoryDTO>> CreateAsync(CategoryCreateDTO dto);
        public Task<Response<List<CategoryDTO>>> GetCompleteListAsync();
    }

}
