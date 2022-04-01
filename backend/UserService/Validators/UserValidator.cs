using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Enitites;

namespace UserService.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(f => f.FirstName).NotEmpty().Matches(@"^[a-zA-Z]+$").WithMessage("First name is required and must be characters only!");
            RuleFor(l => l.LastName).NotEmpty().Matches(@"^[a-zA-Z]+$").WithMessage("Last name required!");
            RuleFor(e => e.Email).NotEmpty().EmailAddress().WithMessage("Email is required!");
            RuleFor(p => p.Password).NotEmpty().Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$").WithMessage("Password wrong");
            
        }
        

    }

   
}
