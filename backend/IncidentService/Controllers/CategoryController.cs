using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IncidentService.Data;
using IncidentService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IncidentService.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task <ActionResult<List<CategoryDto>>> GetCategories()
        {
            var categories = await categoryRepository.GetCategoriesAsync();


            if (categories == null || categories.Count == 0)
            {
                return NoContent();
            }


            List<CategoryDto> categoryDtos = new List<CategoryDto>();

            foreach (var category in categories)
            {
                CategoryDto categoryDto = mapper.Map<CategoryDto>(category);

                categoryDtos.Add(categoryDto);
            }

            return Ok(categoryDtos);
        }
    }
}
