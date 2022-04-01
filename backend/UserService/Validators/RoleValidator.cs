using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Enitites;

namespace UserService.Validators
{
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            List<string> conditions = new List<string>() {"Admin", "User"};

            RuleFor(x => x.UserType).Must(x => conditions.Contains(x)).WithMessage("Significance can only be: " + String.Join(",", conditions) + "!");
            
        }
    }
}
