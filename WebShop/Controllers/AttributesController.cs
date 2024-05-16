using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebShop.Dtos.Read;
using WebShop.Services.Interfaces;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class AttributesController : ControllerBase
    {
        private readonly IAttributeService _attributeService;
        public AttributesController(IAttributeService attributeService)
        {
            _attributeService = attributeService;
        }
        [HttpGet]
        [Route("{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            List<AttributeR> attributeDtos = await _attributeService.GetByCategoryAsync(categoryId);
            return Ok(attributeDtos);
        }
    }
}
