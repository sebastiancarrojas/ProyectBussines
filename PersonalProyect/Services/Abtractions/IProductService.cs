using PersonalProyect.Core;
using PersonalProyect.Core.Pagination;
using PersonalProyect.Data.Abstractions;
using PersonalProyect.DTOs.Products;

namespace PersonalProyect.Services.Abtractions
{
    public interface IProductService
    {
        // Crear producto
        public Task<Response<ProductCreateDTO>> CreateAsync(ProductCreateDTO dto);

        // Obtener producto por id
        public Task<Response<ProductCreateDTO>> GetOneAsync(Guid id);

        // Obtener lista de productos
        public Task<Response<List<ProductCreateDTO>>> GetCompleteListAsync();

        // Desactivar producto
        public Task<Response<object>> DeactivateAsync(Guid id);

        // Editar producto
        public Task<Response<ProductEditDTO>> UpdateAsync(Guid id, ProductEditDTO dto);

        // Lista paginada

        public Task<Response<PaginationResponse<ProductListDTO>>> GetPaginatedListAsync(ProductPaginationRequest request);

        // Buscar producto en registrar venta

        public Task<Response<List<ProductSearchDTO>>> SearchForSaleAsync(string term);

    }
}
