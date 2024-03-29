﻿using System;
using System.Collections.Generic;
using FluentValidation;
using IncidentService.Models;

namespace IncidentService.Validators
{
    public class IncidentValidator : AbstractValidator<IncidentDto>
    {
        public IncidentValidator()
        {
            List<int> conditions = new List<int>() { 1 , 2 , 3 };
            
            RuleFor(x => x.Significance).Must(x => conditions.Contains(x)).WithMessage("Significance can only be: " + String.Join(",", conditions) + "!");
            RuleFor(x => x.Designation).NotEmpty().WithMessage("Designation must be entered!");
            RuleFor(x => x.Workspace).NotEmpty().WithMessage("Place where incident happened must be entered!");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Date when incident happened must be entered!");
            RuleFor(x => x.Time).NotEmpty().WithMessage("Time when incident happened must be entered!");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description of incident must be entered!");
        }
    }
}
