using AutoMapper;
using PersonalProyect.Data.Entities;
using PersonalProyect.DTOs;


namespace PersonalProyect.Core
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Aquí puedes agregar tus configuraciones de mapeo
            CreateMap<ProductDTO, Product>();
            CreateMap<Product, ProductDTO>();
            CreateMap<CustomerDTO, Customer>();
            CreateMap<Customer, CustomerDTO>();
            CreateMap<SaleDetailDTO, SaleDetail>();
            CreateMap<SaleDetail, SaleDetailDTO>();
            CreateMap<SaleDTO, Sale>();
            CreateMap<Sale, SaleDTO>();
            CreateMap<Payment, PaymentDTO>();
            CreateMap<PaymentDTO, Payment>();
            CreateMap<User, AccountUserDTO>().ReverseMap();
            CreateMap<Permission, PermissionDTO>();
            CreateMap<Role, RoleDTO>().ReverseMap();
        }
    }
}
