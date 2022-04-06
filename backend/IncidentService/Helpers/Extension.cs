using IncidentService.Entities;
using IncidentService.Models;

namespace IncidentService
    .Helpers
{
    public static class Extension
    {
        #region
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

        public static CategoryDto CategoryWithIdDtoToDto(this CategoryWithIdDto categoryWithIdDto)
        {
            if (categoryWithIdDto != null)
            {
                return new CategoryDto
                {
                    CategoryName = categoryWithIdDto.CategoryName
                };
            }
            return null;
        }

        public static CategoryWithIdDto DtoToCategoryWithIdDto(this CategoryDto categoryDto)
        {
            if (categoryDto != null)
            {
                return new CategoryWithIdDto
                {
                    CategoryName = categoryDto.CategoryName
                };
            }
            return null;
        }

        public static Category CategoryWithIdDtoToCategory(this CategoryWithIdDto categoryWithIdDto)
        {
            if (categoryWithIdDto != null)
            {
                return new Category
                {
                    CategoryId = categoryWithIdDto.CategoryId,
                    CategoryName = categoryWithIdDto.CategoryName
                };
            }
            return null;
        }

        public static CategoryWithIdDto CategoryToCategoryWithIdDto(this Category category)
        {
            if (category != null)
            {
                return new CategoryWithIdDto
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                };
            }
            return null;
        }
        #endregion

        #region
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

        public static IncidentDto IncidentWithIdDtoToIncidentDto(this IncidentWithIdDto incidentWithIdDto)
        {
            if (incidentWithIdDto != null)
            {
                return new IncidentDto
                {
                    Number = incidentWithIdDto.Number,
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
                    UserId = incidentWithIdDto.UserId,
                    CategoryId = incidentWithIdDto.CategoryId,
                    Category = incidentWithIdDto.Category
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

        public static IncidentWithIdDto IncidentToIncidentWithIdDto(this Incident incident)
        {
            if (incident != null)
            {
                return new IncidentWithIdDto
                {
                    IncidentId = incident.IncidentId,
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

        public static Incident IncidentWithIdDtoToIncident(this IncidentWithIdDto incidentWithIdDto)
        {
            if (incidentWithIdDto != null)
            {
                return new Incident
                {
                    IncidentId = incidentWithIdDto.IncidentId,
                    Number = incidentWithIdDto.Number,
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
                    UserId = incidentWithIdDto.UserId,
                    CategoryId = incidentWithIdDto.CategoryId,
                    Category = incidentWithIdDto.Category
                };
            }
            return null;
        }
        #endregion
    }
}
