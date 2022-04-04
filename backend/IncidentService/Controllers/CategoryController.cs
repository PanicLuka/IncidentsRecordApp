using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using IncidentService.Services;
using IncidentService.Entities;
using IncidentService.Models;
using IncidentService.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace IncidentService.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoriesService categoriesService;
        private readonly IMapper mapper;
        private readonly CategoryValidator categoryValidator;

        public CategoryController(ICategoriesService categoriesService, IMapper mapper, CategoryValidator categoryValidator)
        {
            this.categoriesService = categoriesService;
            this.mapper = mapper;
            this.categoryValidator = categoryValidator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task <ActionResult<List<CategoryDto>>> GetCategories()
        {
            var categories = await categoriesService.GetCategoriesAsync();


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

        [HttpGet("{CategoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDto>> GetCategoryByIdAsync(int CategoryId)
        {
            var category = await categoriesService.GetCategoryByIdAsync(CategoryId);

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

                categoryValidator.ValidateAndThrow(category);

                await categoriesService.CreateCategoryAsync(category);

                await categoriesService.SaveChangesAsync();

                return Ok();
            }
            catch (ValidationException v)
            {
                return StatusCode(StatusCodes.Status400BadRequest, v.Errors);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [HttpPut("{CategoryId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateCategoryAsync(int CategoryId, [FromBody] CategoryDto categoryDto)
        {
            try
            {
                var oldCategory = await categoriesService.GetCategoryByIdAsync(CategoryId);

                if (oldCategory == null)
                {
                    return NotFound();
                }

                Category category = mapper.Map<Category>(categoryDto);

                mapper.Map(category, oldCategory);

                categoryValidator.ValidateAndThrow(category);

                await categoriesService.SaveChangesAsync();

                return Ok(mapper.Map<CategoryDto>(oldCategory));
            }
            catch (ValidationException v)
            {
                return StatusCode(StatusCodes.Status400BadRequest, v.Errors);
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
                var category = await categoriesService.GetCategoryByIdAsync(CategoryId);

                if (category == null)
                {
                    return NotFound();

                }

                await categoriesService.DeleteCategoryAsync(CategoryId);
                await categoriesService.SaveChangesAsync();
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
