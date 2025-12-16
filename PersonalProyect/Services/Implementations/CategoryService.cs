using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonalProyect.Core;
using PersonalProyect.Data;
using PersonalProyect.Data.Entities;
using PersonalProyect.DTOs;
using PersonalProyect.Services.Abtractions;

namespace PersonalProyect.Services.Implementations
{
    public class CategoryService : CustomQueryableOperationsService, ICategoryService
    {
        // ---------------------------
        // -- Inyectar dependencias --
        // ---------------------------

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CategoryService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ---------------------
        // -- Crear categoria --
        // ---------------------

        public async Task<Response<CategoryDTO>> CreateAsync(CategoryCreateDTO dto)
        {
            try
            {
                // 1️⃣ Mapear DTO → Entity
                var entity = _mapper.Map<Category>(dto);

                // 2️⃣ Reglas de negocio (futuras)
                // Ej: validar nombre duplicado
                var existingCategory = await _context.Categories
                    .FirstOrDefaultAsync(c => c.CategoryName == dto.CategoryName);

                if (existingCategory != null)
                {
                    return Response<CategoryDTO>.Failure("La categoría ya existe");
                }

                _context.Categories.Add(entity);
                await _context.SaveChangesAsync();

                // 3️⃣ Mapear Entity → DTO
                var result = _mapper.Map<CategoryDTO>(entity);

                return Response<CategoryDTO>.Success(result);
            }
            catch (Exception ex)
            {
                return Response<CategoryDTO>.Failure(ex, "Error al crear la categoría");
            }
        }

        // -------------------------
        // -- Lista de categorias --
        // -------------------------

        public async Task<Response<List<CategoryDTO>>> GetCompleteListAsync()
        {
            return await GetCompleteListAsync<Category, CategoryDTO>();
        }

    }
}
