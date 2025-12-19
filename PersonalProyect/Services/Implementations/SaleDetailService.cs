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


    }
}
