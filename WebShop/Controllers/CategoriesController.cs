using Microsoft.AspNetCore.Mvc;
using WebShop.Dtos.Read;
using WebShop.Repositories.Interfaces;
using WebShop.Services.Interfaces;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            List<CategoryR> categoryDto = await _categoryService.GetAllAsync();
            return Ok(categoryDto);
        }
    }
}
