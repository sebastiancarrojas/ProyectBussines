using PersonalProyect.Services.Abtractions;
using PersonalProyect.Data;
using AutoMapper;
using PersonalProyect.Core;
using PersonalProyect.DTOs;
using PersonalProyect.Data.Entities;

namespace PersonalProyect.Services.Implementations
{
    public class SaleService : CustomQueryableOperationsService, ISaleService
    {
        // Intectar dependencias necesarias
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SaleService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create Sale
        public async Task<Response<SaleDTO>> CreateAsync(SaleDTO dto)
        {
            return await CreateAsync<Sale, SaleDTO>(dto);
        }

        // Get
        public async Task<Response<SaleDTO>> GetOneAsync(Guid id)
        {
            return await GetOneAsync<Sale, SaleDTO>(id);
        }

        public async Task<Response<List<SaleDTO>>> GetCompleteListAsync()
        {
            return await GetCompleteListAsync<Sale, SaleDTO>();
        }

    }
}
