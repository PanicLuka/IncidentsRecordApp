using IncidentService.Entities;
using IncidentService.Models;

namespace IncidentService.Helpers
{
    public static class IncidentExtension
    {
        public static IncidentDto IncidentToDto(this Incident incident)
        {
            if (incident != null)
            {
                return new IncidentDto
                {
                    Designation = incident.Designation,
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
                    ReportedBy = incident.ReportedBy,
                    UserId = incident.UserId,
                    CategoryId = incident.CategoryId
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
                    Designation = incidentDto.Designation,
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
                    ReportedBy = incidentDto.ReportedBy,
                    UserId = incidentDto.UserId,
                    CategoryId = incidentDto.CategoryId
                };
            }
            return null;
        }

        public static IncidentDto IncidentWithIdDtoToIncidentDto(this IncidentWithIdDto incidentWithIdDto)
        {
            if (incidentWithIdDto != null)
            {
                return new IncidentDto
                {
                    Designation = incidentWithIdDto.Designation,
                    Significance = incidentWithIdDto.Significance,
                    Workspace = incidentWithIdDto.Workspace,
                    Date = incidentWithIdDto.Date,
                    Time = incidentWithIdDto.Time,
                    Description = incidentWithIdDto.Description,
                    ThirdPartyHelp = incidentWithIdDto.ThirdPartyHelp,
                    ProblemSolved = incidentWithIdDto.ProblemSolved,
                    FurtherAction = incidentWithIdDto.FurtherAction,
                    FurtherActionPerson = incidentWithIdDto.FurtherActionPerson,
                    ActionDescription = incidentWithIdDto.ActionDescription,
                    SolvingDate = incidentWithIdDto.SolvingDate,
                    Remarks = incidentWithIdDto.Remarks,
                    Verifies = incidentWithIdDto.Verifies,
                    ReportedBy = incidentWithIdDto.ReportedBy,
                    UserId = incidentWithIdDto.UserId,
                    CategoryId = incidentWithIdDto.CategoryId
                };
            }
            return null;
        }

        public static IncidentWithIdDto DtoToIncidentWithIdDto(this IncidentDto incidentDto)
        {
            if (incidentDto != null)
            {
                return new IncidentWithIdDto
                {
                    Designation = incidentDto.Designation,
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
                    ReportedBy = incidentDto.ReportedBy,
                    UserId = incidentDto.UserId,
                    CategoryId = incidentDto.CategoryId
                };
            }
            return null;
        }

        public static IncidentWithIdDto IncidentToIncidentWithIdDto(this Incident incident)
        {
            if (incident != null)
            {
                return new IncidentWithIdDto
                {
                    IncidentId = incident.IncidentId,
                    Designation = incident.Designation,
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
                    ReportedBy = incident.ReportedBy,
                    UserId = incident.UserId,
                    CategoryId = incident.CategoryId
                };
            }
            return null;
        }

        public static Incident IncidentWithIdDtoToIncident(this IncidentWithIdDto incidentWithIdDto)
        {
            if (incidentWithIdDto != null)
            {
                return new Incident
                {
                    IncidentId = incidentWithIdDto.IncidentId,
                    Designation = incidentWithIdDto.Designation,
                    Significance = incidentWithIdDto.Significance,
                    Workspace = incidentWithIdDto.Workspace,
                    Date = incidentWithIdDto.Date,
                    Time = incidentWithIdDto.Time,
                    Description = incidentWithIdDto.Description,
                    ThirdPartyHelp = incidentWithIdDto.ThirdPartyHelp,
                    ProblemSolved = incidentWithIdDto.ProblemSolved,
                    FurtherAction = incidentWithIdDto.FurtherAction,
                    FurtherActionPerson = incidentWithIdDto.FurtherActionPerson,
                    ActionDescription = incidentWithIdDto.ActionDescription,
                    SolvingDate = incidentWithIdDto.SolvingDate,
                    Remarks = incidentWithIdDto.Remarks,
                    Verifies = incidentWithIdDto.Verifies,
                    ReportedBy =incidentWithIdDto.ReportedBy,
                    UserId = incidentWithIdDto.UserId,
                    CategoryId = incidentWithIdDto.CategoryId
                };
            }
            return null;
        }
    }
}
