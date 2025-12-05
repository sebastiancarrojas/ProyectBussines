using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonalProyect.Core;
using PersonalProyect.Data;
using PersonalProyect.Data.Abstractions;

// Plantilla de servicio para operaciones CRUD 

namespace PersonalProyect.Services
{
    public class CustomQueryableOperationsService
    {
        // Inyectar dependencias necesarias
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CustomQueryableOperationsService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // DELETE // Método genérico para eliminar una entidad basada en su DTO
        public async Task<Response<object>> DeleteAsync<TEntity>(Guid id) where TEntity : class, IId
        {
            try
            {
                // Busca en la tabla correspondiente si existe un registro con ese Id // Si existe lo devuelve // Si no existe devuelve null.
                TEntity? entity = await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
                // Si no se encuentra la entidad (Null), devuelve un error
                if (entity == null)
                {
                    return Response<object>.Failure("Entity not found");
                }

                // Si se encuentra, elimina la entidad
                _context.Remove(entity);
                // Guarda los cambios en la base de datos
                await _context.SaveChangesAsync();

                return Response<object>.Success("Entity deleted successfully");
            }
            catch (Exception ex)
            {
                return Response<object>.Failure(ex, "Error deleting entity");
            }
        }

        // CREATE // Método genérico para crear una entidad basada en su DTO
        public async Task<Response<TDTO>> CreateAsync<TEntity, TDTO>(TDTO dto) where TEntity : IId
        {
            try
            {
                // Mapea el DTO a la entidad correspondiente
                TEntity entity = _mapper.Map<TEntity>(dto);
                // Asigna un nuevo Id a la entidad
                Guid Id = Guid.NewGuid();

                // Asigna el Id generado a la entidad
                entity.Id = Id;

                // Agrega la entidad al contexto y guarda los cambios en la base de datos
                await _context.AddAsync(entity);
                // Guarda los cambios en la base de datos
                await _context.SaveChangesAsync();

                return Response<TDTO>.Success("Entity created successfully");

            }
            catch (Exception ex)
            {
                return Response<TDTO>.Failure(ex, "Error creating entity");
            }
        }

        // UPDATE // Método genérico para actualizar una entidad basada en su DTO
        public async Task<Response<TDTO>> UpdateAsync<TEntity, TDTO>(Guid id, TDTO dto) where TEntity : class, IId
        {
            try
            {
                // Verifica que el Id proporcionado invalido
                if (id == Guid.Empty)
                {
                    return Response<TDTO>.Failure("Invalid ID");
                }

                // Mapea el DTO a la entidad correspondiente
                TEntity entity = _mapper.Map<TEntity>(dto);

                // Asigna el Id proporcionado a la entidad (Esta entidad es la que tiene ese ID)
                entity.Id = id;

                // Marca la entidad como modificada
                _context.Entry(entity).State = EntityState.Modified;

                // Guarda los cambios en la base de datos
                await _context.SaveChangesAsync();

                return Response<TDTO>.Success("Entity updated successfully");
            }
            catch (Exception ex)
            {
                return Response<TDTO>.Failure(ex, "Error updating entity");
            }
        }
        // READ // Método genérico para obtener una entidad por su Id
        public async Task<Response<List<TDTO>>> GetCompleteListAsync<TEntity, TDTO>(IQueryable<TEntity>? query = null) where TEntity : class, IId
        {
            try
            {
                // Si no se proporciona una consulta personalizada, utiliza el conjunto de entidades predeterminado
                if (query == null)
                {
                    // Obtiene todas las entidades del conjunto correspondiente
                    query = _context.Set<TEntity>();
                }

                // Ejecuta la consulta y obtiene la lista de entidades
                List<TEntity> entities = await query.ToListAsync();
                // Mapea las entidades a sus respectivos DTOs
                List<TDTO> dtos = _mapper.Map<List<TDTO>>(entities);

                return Response<List<TDTO>>.Success(dtos);
            }
            catch (Exception ex)
            {
                return Response<List<TDTO>>.Failure(ex, "Error retrieving entities");
            }
        }
        public async Task<Response<TDTO>> GetOneAsync<TEntity, TDTO>(Guid id, IQueryable<TEntity>? query = null) where TEntity : class, IId
        {
            try
            {
                // Si no se proporciona una consulta personalizada, utiliza el conjunto de entidades predeterminado
                if (query is null)
                {
                    query = _context.Set<TEntity>();
                }

                // Ejecuta la consulta para obtener la entidad con el Id especificado
                TEntity? entity = await query.FirstOrDefaultAsync(e => e.Id == id);
                // Si no se encuentra la entidad, devuelve un error
                if (entity is null)
                {
                    return Response<TDTO>.Failure("Entity not found");
                }

                // Mapea la entidad a su respectivo DTO
                TDTO dto = _mapper.Map<TDTO>(entity);

                return Response<TDTO>.Success(dto);
            }
            catch (Exception ex)
            {
                return Response<TDTO>.Failure(ex, "Error retrieving entity");
            }
        }
    }
}
