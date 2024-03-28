using WebShop.Repositories.Implementations;
using WebShop.Repositories.Interfaces;
using WebShop.Services.Implementations;
using WebShop.Services.Interfaces;

namespace WebShop.Helpers
{
    public static class DependencyInjection
    {
        public static void ConfigureDI(IServiceCollection service)
        {
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped<ICartRepository, CartRepository>();
            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<ICommentRepository, CommentRepository>();
            service.AddScoped<IFeedbackRepository, FeedbackRepository>();
            service.AddScoped<IOrderRepository, OrderRepository>();
            service.AddScoped<ISubcategoryRepository, SubcategoryRepository>(); 
            service.AddScoped<IAttributeRepository, AttributeRepository>();

            service.AddScoped<IProductService, ProductService>();
        }
    }
}
