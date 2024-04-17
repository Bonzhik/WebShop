﻿using WebShop.Repositories.Implementations;
using WebShop.Repositories.Interfaces;
using WebShop.Services.ImageService;
using WebShop.Services.Implementations;
using WebShop.Services.Interfaces;
using WebShop.Services.PaginationService;

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
            service.AddScoped<IStatusRepository, StatusRepository>();

            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<ICategoryService, CategoryService>();
            service.AddScoped<IAttributeService, AttributeService>();
            service.AddScoped<IFeedbackService, FeedbackService>();
            service.AddScoped<ICommentService, CommentService>();
            service.AddScoped<IOrderService, OrderService>();
            service.AddScoped<IImageService, ImageService>();
            service.AddScoped(typeof(IPaginationService<>), typeof(PaginationService<>));
            service.AddScoped<ICartService, CartService>();
            service.AddHttpClient<ImageService>();

            service.AddScoped<IAccountService, AccountService>();
        }
    }
}
