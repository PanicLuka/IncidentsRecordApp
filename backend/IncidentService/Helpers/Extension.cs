using IncidentService.Entities;
using IncidentService.Models;

namespace IncidentService
    .Helpers
{
    public static class Extension
    {
        public static CategoryDto CategoryToDto(this Category category)
        {
            if (category != null)
            {
                return new CategoryDto
                {
                    CategoryName = category.CategoryName
                };
            }
            return null;
        }
        public static Category DtoToCategory(this CategoryDto categoryDto)
        {
            if (categoryDto != null)
            {
                return new Category
                {
                    CategoryName = categoryDto.CategoryName
                };
            }
            return null;
        }

        public static IncidentDto IncidentToDto(this Incident incident)
        {
            if (incident != null)
            {
                return new IncidentDto
                {
                    Number = incident.Number,
                    Significance = incident.Significance,
                    Workspace = incident.Workspace,
                    Date = incident.Date,
                    Time = incident.Time,
                    Description = incident.Description,
                    ThirdPartyHelp = incident.ThirdPartyHelp,
                    ProblemSolved = incident.ProblemSolved,
                    FurtherAction = incident.FurtherAction,
                    FurtherActionPerson = incident.FurtherActionPerson,
                    ActionDescription = incident.ActionDescription,
                    SolvingDate = incident.SolvingDate,
                    Remarks = incident.Remarks,
                    Verifies = incident.Verifies,
                    UserId = incident.UserId,
                    CategoryId = incident.CategoryId,
                    Category = incident.Category
                };
            }
            return null;
        }
        public static Incident DtoToIncident(this IncidentDto incidentDto)
        {
            if (incidentDto != null)
            {
                return new Incident
                {
                    Number = incidentDto.Number,
                    Significance = incidentDto.Significance,
                    Workspace = incidentDto.Workspace,
                    Date = incidentDto.Date,
                    Time = incidentDto.Time,
                    Description = incidentDto.Description,
                    ThirdPartyHelp = incidentDto.ThirdPartyHelp,
                    ProblemSolved = incidentDto.ProblemSolved,
                    FurtherAction = incidentDto.FurtherAction,
                    FurtherActionPerson = incidentDto.FurtherActionPerson,
                    ActionDescription = incidentDto.ActionDescription,
                    SolvingDate = incidentDto.SolvingDate,
                    Remarks = incidentDto.Remarks,
                    Verifies = incidentDto.Verifies,
                    UserId = incidentDto.UserId,
                    CategoryId = incidentDto.CategoryId,
                    Category = incidentDto.Category
                };
            }
            return null;
        }
    }
}
