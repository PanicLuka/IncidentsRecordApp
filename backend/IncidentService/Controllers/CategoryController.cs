using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using IncidentService.Data;
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
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly CategoryValidator categoryValidator;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper, CategoryValidator categoryValidator)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.categoryValidator = categoryValidator;
        }

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
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

        [HttpGet("{CategoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

                categoryValidator.ValidateAndThrow(category);

                await categoryRepository.CreateCategoryAsync(category);

                await categoryRepository.SaveChangesAsync();

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
                var oldCategory = await categoryRepository.GetCategoryByIdAsync(CategoryId);

                if (oldCategory == null)
                {
                    return NotFound();
                }

                Category category = mapper.Map<Category>(categoryDto);

                mapper.Map(category, oldCategory);

                categoryValidator.ValidateAndThrow(category);

                await categoryRepository.SaveChangesAsync();

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
