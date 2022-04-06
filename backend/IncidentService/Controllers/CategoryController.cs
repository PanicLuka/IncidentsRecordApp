using System;
using System.Collections.Generic;
using FluentValidation;
using IncidentService.Services;
using IncidentService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;

namespace IncidentService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoriesService categoriesService;

        public CategoryController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<CategoryDto>> GetCategories()
        {
            var categoryDtos = categoriesService.GetCategories();

            if (categoryDtos == null || categoryDtos.Count == 0)
            {
                return NoContent();
            }

            return Ok(categoryDtos);
        }

        [HttpGet("{CategoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CategoryWithIdDto> GetCategoryById(Guid CategoryId)
        {
            var categoryDto = categoriesService.GetCategoryById(CategoryId);

            if (categoryDto == null)
            {
                return NotFound();
            }

            return Ok(categoryDto);

        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreateCategory([FromBody] CategoryDto categoryDto)
        {
            try
            { 
                categoriesService.CreateCategory(categoryDto);

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
        public ActionResult UpdateCategory(Guid CategoryId, [FromBody] CategoryDto categoryDto)
        {
            try
            {
                var newCategory = categoriesService.UpdateCategory(CategoryId, categoryDto);

                if (newCategory == null)
                {
                    return NotFound();
                }

                return Ok(newCategory);
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
        public IActionResult DeleteCategory(Guid CategoryId)
        {
            try
            {
                var category = categoriesService.GetCategoryById(CategoryId);

                if (category == null)
                {
                    return NotFound();
                }

                categoriesService.DeleteCategory(CategoryId);
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
