using PersonalProyect.Core.Pagination;
using PersonalProyect.DTOs.Products;
using PersonalProyect.DTOs.Brands;
using PersonalProyect.DTOs.Categories;

namespace PersonalProyect.ViewModels.Products
{
    public class ProductsIndexViewModel
    {
        public PaginationResponse<ProductListDTO> Products { get; set; } = new PaginationResponse<ProductListDTO>();
        public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
        public List<BrandDTO> Brands { get; set; } = new List<BrandDTO>();
    }
}
