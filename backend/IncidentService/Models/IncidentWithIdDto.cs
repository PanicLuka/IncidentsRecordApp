using System;
using IncidentService.Entities;

namespace IncidentService.Models
{
    public class IncidentWithIdDto
    {
        public Guid IncidentId { get; set; }
        public string Number { get; set; }
        public int Significance { get; set; }
        public string Workspace { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }
        public bool ThirdPartyHelp { get; set; }
        public string ProblemSolved { get; set; }
        public bool FurtherAction { get; set; }
        public string FurtherActionPerson { get; set; }
        public string ActionDescription { get; set; }
        public DateTime SolvingDate { get; set; }
        public string Remarks { get; set; }
        public string Verifies { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
