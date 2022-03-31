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

        //returns new but doesnt change anything in database, and returns new with id 0
        /*[HttpPut("{CategoryId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryUpdateDto>> UpdateCategoryAsync(int CategoryId, [FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            try
            {
                var oldCategory = await categoryRepository.GetCategoryByIdAsync(CategoryId);

                if (oldCategory == null)
                {
                    return NotFound();
                }

                mapper.Map(categoryUpdateDto, oldCategory);

                await categoryRepository.UpdateCategoryAsync(mapper.Map<Category>(categoryUpdateDto));

                return Ok(categoryUpdateDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }*/

        //returns but old
        /*[HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryDto>> UpdateCategoryAsync(CategoryUpdateDto categoryUpdate)
        {
            try
            {
                var oldCategory = await categoryRepository.GetCategoryByIdAsync(categoryUpdate.CategoryId);

                if (oldCategory == null)
                {
                    return NotFound();
                }

                Category category = mapper.Map<Category>(categoryUpdate);

                mapper.Map(category, oldCategory);

                await categoryRepository.SaveChangesAsync();

                return Ok(mapper.Map<CategoryDto>(oldCategory));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }*/

        /*
        [HttpPut("{CategoryId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Category>> UpdateCategoryAsync(int categoryId, [FromBody] Category category)
        {
            try
            {
                var oldCategory = await categoryRepository.GetCategoryByIdAsync(categoryId);

                if (oldCategory == null)
                {
                    return NotFound();
                }

                mapper.Map(category, oldCategory);

                await categoryRepository.UpdateCategoryAsync(category);

                return Ok(category);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }*/

        /*
        //mapping error
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task <ActionResult<CategoryUpdateDto>> UpdateCategoryAsync(CategoryUpdateDto categoryDto)
        {
            try
            {
                var oldCategory = categoryRepository.GetCategoryByIdAsync(categoryDto.CategoryId);
                if (oldCategory == null)
                {
                    return NotFound();
                }
                Category categoryEntity = mapper.Map<Category>(categoryDto);

                await mapper.Map(categoryEntity, oldCategory);             

                await categoryRepository.SaveChangesAsync();
                return Ok(mapper.Map<CategoryDto>(oldCategory));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }*/

        /*
        [HttpPut]
        public ActionResult<CategoryDto> UpdateCategory(CategoryUpdateDto category)
        {
            try
            {
                if (categoryRepository.GetCategoryByIdAsync(category.CategoryId) == null)
                {
                    return NotFound();
                }
                Category categoryEntity = mapper.Map<Category>(category);
                categoryRepository.UpdateCategoryAsync(categoryEntity);
                categoryRepository.SaveChangesAsync();
                return Ok(mapper.Map<CategoryDto>(categoryEntity));
            }
            catch (Exception e)
            { 
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }*/

        /*
        //The property 'Category.CategoryId' is part of a key and so cannot be modified or marked as modified. 
        //To change the principal of an existing entity with an identifying foreign key, 
        //first delete the dependent and invoke 'SaveChanges', and then associate the dependent with the new principal.
        [HttpPut("{CategoryId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateCategoryAsync(int CategoryId, [FromBody] CategoryUpdateDto categoryDto)
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
                await categoryRepository.SaveChangesAsync();
                return Ok(mapper.Map<CategoryDto>(oldCategory));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }*/

        /*
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Category>> UpdateCategoryAsync(int id, Category category)
        {
            try
            {
                if (id != category.CategoryId)
                {
                    return BadRequest("ID mismatch");
                }

                var categoryToUpdate = await categoryRepository.GetCategoryByIdAsync(id);

                if (categoryToUpdate == null)
                {
                    return NotFound();
                }

                return Ok(category);
                //return await categoryRepository.UpdateCategoryAsync(category);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }*/


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
