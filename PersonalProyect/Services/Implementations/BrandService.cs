using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonalProyect.Core;
using PersonalProyect.Data;
using PersonalProyect.Data.Entities;
using PersonalProyect.DTOs;
using PersonalProyect.Services.Abtractions;

namespace PersonalProyect.Services.Implementations
{
    public class BrandService : CustomQueryableOperationsService, IBrandService
    {
        // -------------------------------
        // -- Inyeccion de dependencias --
        // -------------------------------

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public BrandService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // -----------------
        // -- Crear marca --
        // -----------------

        public async Task<Response<BrandDTO>> CreateAsync(BrandCreateDTO dto)
        {
            try
            {
                // 1️⃣ Mapear DTO → Entity
                var entity = _mapper.Map<Brand>(dto);

                // 2️⃣ Reglas de negocio (futuras)
                // Ej: validar nombre duplicado
                var existingBrand = await _context.Brands
                    .FirstOrDefaultAsync(c => c.BrandName == dto.BrandName);

                if (existingBrand != null)
                {
                    return Response<BrandDTO>.Failure("La categoría ya existe");
                }

                _context.Brands.Add(entity);
                await _context.SaveChangesAsync();

                // 3️⃣ Mapear Entity → DTO
                var result = _mapper.Map<BrandDTO>(entity);

                return Response<BrandDTO>.Success(result);
            }
            catch (Exception ex)
            {
                return Response<BrandDTO>.Failure(ex, "Error al crear la categoría");
            }
        }

        // ---------------------
        // -- Lista de marcas --
        // ---------------------

        public async Task<Response<List<BrandDTO>>> GetCompleteListAsync()
        {
            return await GetCompleteListAsync<Brand, BrandDTO>();
        }
    }
}
