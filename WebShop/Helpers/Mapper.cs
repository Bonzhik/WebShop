using AutoMapper;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Models;

namespace WebShop.Helpers
{
    public class Mapper : Profile
    {
        public Mapper()
        {

            CreateMap<Product, ProductR>()
                .ForMember(dest => dest.SubcategoryTitle, opt => opt.MapFrom(src => src.Subcategory.Title))
                .ForMember(dest => dest.CategoryTitle, opt => opt.MapFrom(src => src.Subcategory.Category.Title))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Subcategory.Category.Id));

            CreateMap<ProductW, Product>()
                .ForMember(dest => dest.AttributeValues, opt => opt.Ignore());

            CreateMap<User, UserR>();
            CreateMap<Feedback, FeedbackR>();
            CreateMap<Comment, CommentR>();
            CreateMap<Subcategory, SubcategoryR>();
            CreateMap<Category, CategoryR>();
            CreateMap<Models.Attribute, AttributeR>();
        }
    }
}
