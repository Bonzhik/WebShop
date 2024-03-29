using AutoMapper;
using WebShop.Dtos.Read;
using WebShop.Models;
using WebShop.Repositories.Interfaces;
using WebShop.Services.Interfaces;

namespace WebShop.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {

            _categoryRepository = categoryRepository;
            _mapper = mapper;

        }

        public async Task<List<CategoryR>> GetAllAsync()
        {
            List<Category> categories = await _categoryRepository.GetAllAsync();

            List<CategoryR> categoryDtos = new List<CategoryR>();
            foreach (var category in categories)
            {
                categoryDtos.Add(MapToDto(category));
            }

            return categoryDtos;

        }
        private CategoryR MapToDto(Category category)
        {
            CategoryR categoryDto = _mapper.Map<CategoryR>(category);
            categoryDto.Subcategories = _mapper.Map<List<SubcategoryR>>(category.Subcategories);

            return categoryDto;
        }
    }
}
