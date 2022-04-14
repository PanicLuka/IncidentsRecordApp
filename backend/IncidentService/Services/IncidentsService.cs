using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using FluentValidation;
using IncidentService.Entities;
using IncidentService.Helpers;
using IncidentService.Models;
using IncidentService.Validators;

namespace IncidentService.Services
{
    public class IncidentsService : IIncidentsService
    {
        private readonly DataContext _context;
        private readonly IncidentValidator _incidentValidator = new IncidentValidator();
        private static int _count;
        public IncidentsService(DataContext context)
        {
            _context = context;
        }
        public IncidentDto CreateIncident(IncidentDto incidentDto, Guid userId)
        {
            Incident incident = incidentDto.DtoToIncident();

            incident.UserId = userId;

            _incidentValidator.ValidateAndThrow(incidentDto);

            _context.Add(incident);

            SaveChanges();

            return incident.IncidentToDto();
        }

        public void DeleteIncident(Guid id)
        {
            var incident = GetIncidentForUpdateById(id);

            if (incident == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(incident);

            SaveChanges();
        }

        public IncidentWithIdDto GetIncidentById(Guid id)
        {
            Incident incident = _context.Incidents.FirstOrDefault(e => e.IncidentId == id);

            IncidentWithIdDto incidentWithIdDto = incident.IncidentToIncidentWithIdDto();

            if (incidentWithIdDto == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return incidentWithIdDto;
        }

        public List<IncidentDto> GetIncidents(IncidentOpts incidentOpts)
        {
            var filteredIncidents = FilterIncidents(incidentOpts);

            List<Incident> incidents = filteredIncidents.ToList();

            if (incidents == null)
            {
                throw new HttpResponseException(HttpStatusCode.NoContent);
            }

            List<IncidentDto> incidentDtos = incidents.Select(incident => incident.IncidentToDto()).ToList();

            return incidentDtos;
        }

        private IQueryable<Incident> FilterIncidents(IncidentOpts incidentOpts) {

            IQueryable<Incident> incidentList = _context.Incidents;

            if (incidentOpts.FirstDate.HasValue)
            {
                incidentList = incidentList.Where(o => o.Date >= incidentOpts.FirstDate).AsQueryable();
            }
            if (incidentOpts.SecondDate.HasValue)
            {
                incidentList = incidentList.Where(o => o.Date <= incidentOpts.SecondDate).AsQueryable();
            }
            if (incidentOpts.FirstSolvingDate.HasValue)
            {
                incidentList = incidentList.Where(o => o.SolvingDate >= incidentOpts.FirstSolvingDate).AsQueryable();
            }
            if (incidentOpts.SecondSolvingDate.HasValue)
            {
                incidentList = incidentList.Where(o => o.SolvingDate <= incidentOpts.SecondSolvingDate).AsQueryable();
            }
            if (incidentOpts.Significance.HasValue)
            {
                incidentList = incidentList.Where(o => o.Significance == incidentOpts.Significance).AsQueryable();
            }
            if (incidentOpts.FurtherAction.HasValue)
            {
                incidentList = incidentList.Where(o => o.FurtherAction == incidentOpts.FurtherAction).AsQueryable();
            }
            if (incidentOpts.ThirdPartyHelp.HasValue)
            {
                incidentList = incidentList.Where(o => o.FurtherAction == incidentOpts.ThirdPartyHelp).AsQueryable();
            }
            if (incidentOpts.ExactDate.HasValue)
            {
                incidentList = incidentList.Where(o => o.Date == incidentOpts.ExactDate).AsQueryable();
            }
            if (incidentOpts.ExactSolvingDate.HasValue)
            {
                incidentList = incidentList.Where(o => o.SolvingDate == incidentOpts.ExactSolvingDate).AsQueryable();
            }

            _count = incidentList.Count();

            incidentList = incidentList.Skip((incidentOpts.PageNumber - 1) * incidentOpts.PageSize).Take(incidentOpts.PageSize).AsQueryable();

            return incidentList;
        }

        public int GetIncidentCount()
        {
            return _count;
        }

        private Incident GetIncidentForUpdateById(Guid id)
        {
            Incident incident = _context.Incidents.FirstOrDefault(e => e.IncidentId == id);

            if (incident == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return incident;
        }

        public IncidentDto UpdateIncident(Guid IncidentId, IncidentDto incidentDto)
        {
            var oldIncident = GetIncidentForUpdateById(IncidentId);

            if (oldIncident == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                Incident incident = incidentDto.DtoToIncident();

                oldIncident.Designation = incidentDto.Designation;
                oldIncident.Significance = incidentDto.Significance;
                oldIncident.Workspace = incidentDto.Workspace;
                oldIncident.Date = incidentDto.Date;
                oldIncident.Time = incidentDto.Time;
                oldIncident.Description = incidentDto.Description;
                oldIncident.ThirdPartyHelp = incidentDto.ThirdPartyHelp;
                oldIncident.ProblemSolved = incidentDto.ProblemSolved;
                oldIncident.FurtherAction = incidentDto.FurtherAction;
                oldIncident.FurtherActionPerson = incidentDto.FurtherActionPerson;
                oldIncident.ActionDescription = incidentDto.ActionDescription;
                oldIncident.SolvingDate = incidentDto.SolvingDate;
                oldIncident.Remarks = incidentDto.Remarks;
                oldIncident.Verifies = incidentDto.Verifies;
                oldIncident.UserId = incidentDto.UserId;
                oldIncident.CategoryId = incidentDto.CategoryId;
                oldIncident.Category = incidentDto.Category;

                _incidentValidator.ValidateAndThrow(incidentDto);

                SaveChanges();

                return oldIncident.IncidentToDto();
            }
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

    }
}
