using AutoMapper;
using CRUDApp.DTOs;
using CRUDApp.DTOs.Category.CRUDApp.DTOs;
using CRUDApp.Model;

namespace CRUDApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryCreateDTO, Category>();
            
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<ProductCreateDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>();
        }
    }
}
