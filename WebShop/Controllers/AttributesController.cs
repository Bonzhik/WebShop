using Microsoft.AspNetCore.Mvc;
using WebShop.Dtos.Read;
using WebShop.Services.Interfaces;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttributesController : ControllerBase
    {
        private readonly IAttributeService _attributeService;
        public AttributesController(IAttributeService attributeService)
        {
            _attributeService = attributeService;
        }
        [HttpGet]
        [Route("{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId) 
        {
            List<AttributeR> attributeDtos = await _attributeService.GetByCategoryAsync(categoryId);
            return Ok(attributeDtos);
        }
    }
}
