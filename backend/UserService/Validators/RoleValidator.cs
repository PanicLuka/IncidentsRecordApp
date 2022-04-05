using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Enitites;
using UserService.Models;

namespace UserService.Validators
{
    public class RoleValidator : AbstractValidator<RoleDto>
    {
        public RoleValidator()
        {
            List<string> conditions = new List<string>() { "Admin", "User" };

            RuleFor(x => x.UserType).Must(x => conditions.Contains(x)).WithMessage("Significance can only be: " + String.Join(",", conditions) + "!");

        }
    }
}
