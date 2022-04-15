using IncidentService.Entities;
using IncidentService.Models;

namespace IncidentService.Helpers
{
    public static class CategoryExtension
    {
        public static CategoryDto CategoryToDto(this Category category)
        {
            if (category != null)
            {
                return new CategoryDto
                {
                    CategoryName = category.CategoryName
                };
            }
            return null;
        }

        public static Category DtoToCategory(this CategoryDto categoryDto)
        {
            if (categoryDto != null)
            {
                return new Category
                {
                    CategoryName = categoryDto.CategoryName
                };
            }
            return null;
        }

        public static CategoryDto CategoryWithIdDtoToDto(this CategoryWithIdDto categoryWithIdDto)
        {
            if (categoryWithIdDto != null)
            {
                return new CategoryDto
                {
                    CategoryName = categoryWithIdDto.CategoryName
                };
            }
            return null;
        }

        public static CategoryWithIdDto DtoToCategoryWithIdDto(this CategoryDto categoryDto)
        {
            if (categoryDto != null)
            {
                return new CategoryWithIdDto
                {
                    CategoryName = categoryDto.CategoryName
                };
            }
            return null;
        }

        public static Category CategoryWithIdDtoToCategory(this CategoryWithIdDto categoryWithIdDto)
        {
            if (categoryWithIdDto != null)
            {
                return new Category
                {
                    CategoryId = categoryWithIdDto.CategoryId,
                    CategoryName = categoryWithIdDto.CategoryName
                };
            }
            return null;
        }

        public static CategoryWithIdDto CategoryToCategoryWithIdDto(this Category category)
        {
            if (category != null)
            {
                return new CategoryWithIdDto
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                };
            }
            return null;
        }
    }
}
