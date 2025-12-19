using AutoMapper;
using PersonalProyect.Data.Entities;
using PersonalProyect.DTOs;
using PersonalProyect.DTOs.Products;

namespace PersonalProyect.Core
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // -------------------
            // Create
            // -------------------
            CreateMap<ProductCreateDTO, Product>();
            CreateMap<Product, ProductCreateDTO>();

            // -------------------
            // Edit (PATCH seguro)
            // -------------------
            CreateMap<ProductEditDTO, Product>()
                .ForMember(d => d.BrandId, opt => opt.Ignore())
                .ForMember(d => d.CategoryId, opt => opt.Ignore())
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) => srcMember != null)
                );

            CreateMap<Product, ProductEditDTO>();

            // -------------------
            // Otros mapeos
            // -------------------
            CreateMap<CustomerDTO, Customer>().ReverseMap();
            CreateMap<User, AccountUserDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<Permission, PermissionDTO>();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Brand, BrandDTO>().ReverseMap();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<BrandCreateDTO, Brand>();
        }
    }
}
