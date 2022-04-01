using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IncidentService.Data;
using IncidentService.Entities;
using IncidentService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

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


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{CategoryId}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryByIdAsync(int CategoryId)
        {
            var category = await categoryRepository.GetCategoryByIdAsync(CategoryId);

            if (category == null)
            {
                return NotFound();
            }

            CategoryDto categoryDto = mapper.Map<CategoryDto>(category);

            return Ok(categoryDto);

        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateCategoryAsync([FromBody] CategoryDto categoryDto)
        {
            try
            {
                Category category = mapper.Map<Category>(categoryDto);

                await categoryRepository.CreateCategoryAsync(category);

                await categoryRepository.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        


        [HttpDelete("{CategoryId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategoryAsync(int CategoryId)
        {
            try
            {
                var category = await categoryRepository.GetCategoryByIdAsync(CategoryId);

                if (category == null)
                {
                    return NotFound();

                }

                await categoryRepository.DeleteCategoryAsync(CategoryId);
                await categoryRepository.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpOptions]
        public IActionResult GetCategoryOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
