using FluentValidation;
using IncidentService.Entities;

namespace IncidentService.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Category name must be entered!");
        }
    }
}
