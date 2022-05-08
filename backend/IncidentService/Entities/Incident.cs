using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentService.Entities
{
    public class Incident
    {
        [Key]
        public Guid IncidentId { get; set; }
        public string Designation { get; set; }
        public int Significance { get; set; }
        public string Workspace { get; set; }
        public DateTime Date { get; set; }
        public String Time { get; set; }
        public string Description { get; set; }
        public bool ThirdPartyHelp { get; set; }
        public string ProblemSolved { get; set; }
        public bool FurtherAction { get; set; }
        public string FurtherActionPerson { get; set; }
        public string ActionDescription { get; set; }
        public DateTime SolvingDate { get; set; }
        public string Remarks { get; set; }
        public string Verifies { get; set; }
        public string ReportedBy { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("FK_CategoryId")]
        public Guid CategoryId { get; set; }

    }
}
