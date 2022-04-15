using FluentValidation;
using UserService.Models;
using EnumClassLibrary;
using System.Collections.Generic;
using System;

namespace UserService.Validators
{
    public class PermissionValidator : AbstractValidator<PermissionDto>
    {
        public PermissionValidator()
        {
            List<string> conditions = new List<string>();

            foreach (string i in Enum.GetNames(typeof(EnumPermissions.Permissions)))
            {
                conditions.Add(i);
            }

            RuleFor(x => x.AccessPermission).Must(x => conditions.Contains(x))
                .WithMessage("Significance can only be: " + String.Join(",", conditions) + "!");
        }
    }
}
