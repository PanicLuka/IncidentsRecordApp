using FluentValidation;
using IncidentService.Models;

namespace IncidentService.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Category name must be entered!");
        }
    }
}
