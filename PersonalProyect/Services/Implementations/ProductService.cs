using AutoMapper;
using PersonalProyect.Data;
using PersonalProyect.Core;
using PersonalProyect.DTOs;
using PersonalProyect.Data.Entities;
using PersonalProyect.Services.Abtractions;

namespace PersonalProyect.Services.Implementations
{
    public class ProductService : CustomQueryableOperationsService, IProductService
    {
        // Inyectar dependencias necesarias
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ProductService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create Product
        public async Task<Response<ProductDTO>> CreateAsync(ProductDTO dto)
        {
            return await CreateAsync<Product, ProductDTO>(dto);
        }

        // Delete Product
        public async Task<Response<object>> DeleteAsync(Guid id)
        {
            return await DeleteAsync<Product>(id);
        }

        // Update Product
        public async Task<Response<ProductDTO>> UpdateAsync(Guid id, ProductDTO dto)
        {
            return await UpdateAsync<Product, ProductDTO>(id, dto);
        }

        // Get 
        public async Task<Response<ProductDTO>> GetOneAsync(Guid id)
        {
            return await GetOneAsync<Product, ProductDTO>(id);
        }

        public async Task<Response<List<ProductDTO>>> GetCompleteListAsync()
        {
            return await GetCompleteListAsync<Product, ProductDTO>();
        }
    }
}
