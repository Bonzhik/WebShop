

using AutoMapper;
using WebShop.Dtos.Read;
using WebShop.Models;
using WebShop.Repositories.Interfaces;
using WebShop.Services.Interfaces;

namespace WebShop.Services.Implementations
{
    public class AttributeService : IAttributeService
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public AttributeService
            (
            IAttributeRepository attributeRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper
            )
        {
            _attributeRepository = attributeRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<AttributeR>> GetByCategoryAsync(int categoryId)
        {
            Category category = await _categoryRepository.GetAsync(categoryId);
            List<Models.Attribute> attributes = await _attributeRepository.GetByCategoryAsync(category);

            List<AttributeR> attributeDtos = _mapper.Map<List<AttributeR>>(attributes);  

            return attributeDtos;
        }
    }
}
