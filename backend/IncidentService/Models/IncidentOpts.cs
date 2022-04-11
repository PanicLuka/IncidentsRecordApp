using System;

namespace IncidentService.Models
{
    public class IncidentOpts : PaginationQueryStringOpts
    {
        public int? Significance { get; set; }
        public DateTime? FirstDate { get; set; }
        public DateTime? SecondDate { get; set; }
        public DateTime? ExactDate { get; set; }
        public DateTime? FirstTime { get; set; }
        public DateTime? SecondTime { get; set; }
        public bool? ThirdPartyHelp { get; set; }
        public bool? FurtherAction { get; set; }
        public DateTime? FirstSolvingDate { get; set; }
        public DateTime? SecondSolvingDate { get; set; }
        public DateTime? ExactSolvingDate { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
