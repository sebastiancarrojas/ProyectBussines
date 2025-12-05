using PersonalProyect.Data;
using PersonalProyect.Services.Abtractions;
using AutoMapper;
using PersonalProyect.Core;
using PersonalProyect.DTOs;
using PersonalProyect.Data.Entities;

namespace PersonalProyect.Services.Implementations
{
    public class SaleDetailService : CustomQueryableOperationsService, ISaleDetail
    {
        // Inyectar dependencias
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SaleDetailService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create SaleDetail
        public async Task<Response<SaleDetailDTO>> CreateAsync(SaleDetailDTO dto)
        {
            return await CreateAsync<SaleDetail, SaleDetailDTO>(dto);
        }

        // Delete SaleDetail
        public async Task<Response<object>> DeleteAsync(Guid id)
        {
            return await DeleteAsync<SaleDetail>(id);
        }

        // Update SaleDetail
        public async Task<Response<SaleDetailDTO>> UpdateAsync(Guid id, SaleDetailDTO dto)
        {
            return await UpdateAsync<SaleDetail, SaleDetailDTO>(id, dto);
        }

        // Get 
        public async Task<Response<SaleDetailDTO>> GetOneAsync(Guid id)
        {
            return await GetOneAsync<SaleDetail, SaleDetailDTO>(id);
        }

        public async Task<Response<List<SaleDetailDTO>>> GetCompleteListAsync()
        {
            return await GetCompleteListAsync<SaleDetail, SaleDetailDTO>();
        }
    }
}
