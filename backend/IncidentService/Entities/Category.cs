﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IncidentService.Entities
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
