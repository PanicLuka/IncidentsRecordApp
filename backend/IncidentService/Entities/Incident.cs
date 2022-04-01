using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IncidentService.Models;

namespace IncidentService.Entities
{
    public class Incident
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IncidentId { get; set; }
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
        //[ForeignKey("FK_UserId")]
        public int UserId { get; set; }
        [ForeignKey("FK_CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
