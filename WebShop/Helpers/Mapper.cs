using AutoMapper;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Models;

namespace WebShop.Helpers
{
    public class Mapper : Profile
    {
        public Mapper() {
            CreateMap<Product, ProductR>()
                .ForMember(dest => dest.Subcategory, opt => opt.MapFrom(src => src.Subcategory.Title))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Subcategory.Category.Title));
            CreateMap<ProductW, Product>();
            CreateMap<Subcategory, SubcategoryR>();
        }
    }
}
