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
        private readonly ILogger _logger;
        public AttributesController(IAttributeService attributeService, ILogger<AttributesController> logger)
        {
            _attributeService = attributeService;
            _logger = logger;
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
