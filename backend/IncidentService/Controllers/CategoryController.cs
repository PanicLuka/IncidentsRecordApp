﻿using System;
using System.Collections.Generic;
using FluentValidation;
using IncidentService.Services;
using IncidentService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using IncidentService.Attributes;

namespace IncidentService.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : AbstractController
    {
        private readonly ICategoriesService _categoriesService;

        public CategoryController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [MicroserviceAuth]
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<CategoryDto>> GetCategories([FromQuery] CategoryOpts categoryOpts)
        {
            try
            {
                var categoryDtos = _categoriesService.GetCategories(categoryOpts);

                return Ok(categoryDtos);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [MicroserviceAuth]
        [Route("count")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<int> GetCategoryCount()
        {
            try
            {
                var count = _categoriesService.GetCategoryCount();

                return Ok(count);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [MicroserviceAuth]
        [HttpGet("{CategoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CategoryWithIdDto> GetCategoryById(Guid CategoryId)
        {
            try
            {
                var categoryDto = _categoriesService.GetCategoryById(CategoryId);

                return Ok(categoryDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [MicroserviceAuth]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreateCategory([FromBody] CategoryDto categoryDto)
        {
            try
            { 
                if (ModelState.IsValid)
                {
                    var createdCategory = _categoriesService.CreateCategory(categoryDto);

                    return Ok(createdCategory);
                }
                else
                {
                    return BadRequest(ModelState);
                }
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

        [MicroserviceAuth]
        [HttpPut("{CategoryId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CategoryDto> UpdateCategory(Guid CategoryId, [FromBody] CategoryDto categoryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newCategory = _categoriesService.UpdateCategory(CategoryId, categoryDto);

                    return Ok(newCategory);
                }
                else
                {
                    return BadRequest(ModelState);
                }
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

        [MicroserviceAuth]
        [HttpDelete("{CategoryId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCategory(Guid CategoryId)
        {
            try
            {
                var category = _categoriesService.GetCategoryById(CategoryId);

                _categoriesService.DeleteCategory(CategoryId);

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
