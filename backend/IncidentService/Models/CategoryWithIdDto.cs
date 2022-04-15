using System;

namespace IncidentService.Models
{
    public class CategoryWithIdDto
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
