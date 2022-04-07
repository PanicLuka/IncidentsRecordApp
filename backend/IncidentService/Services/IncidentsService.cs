using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation;
using IncidentService.Entities;
using IncidentService.Helpers;
using IncidentService.Models;
using IncidentService.Validators;
using Microsoft.EntityFrameworkCore;

namespace IncidentService.Services
{
    public class IncidentsService : IIncidentsService
    {
        private readonly DataContext context;
        private readonly IncidentValidator incidentValidator = new IncidentValidator();

        public IncidentsService(DataContext context)
        {
            this.context = context;
        }
        public void CreateIncident(IncidentDto incidentDto, Guid userId)
        {
            Incident incident = incidentDto.DtoToIncident();

            incident.UserId = userId;

            incidentValidator.ValidateAndThrow(incidentDto);

            context.Add(incident);

            SaveChanges();
        }

        public void DeleteIncident(Guid id)
        {
            var incident = GetIncidentForUpdateById(id);
            context.Remove(incident);
            SaveChanges();
        }

        public IncidentWithIdDto GetIncidentById(Guid id)
        {
            Incident incident = context.Incidents.FirstOrDefault(e => e.IncidentId == id);

            IncidentWithIdDto incidentWithIdDto = incident.IncidentToIncidentWithIdDto();

            return incidentWithIdDto;
        }

        public PagedList<IncidentDto> GetIncidents(IncidentParameters incidentParameters)
        {
            
            //List<Incident> incidents = context.Incidents.ToList();

            var incidents = GetIncidentsByCondition(o => o.Date >= incidentParameters.FirstDate && o.Date <= incidentParameters.SecondDate);

            List<IncidentDto> incidentDtos = new List<IncidentDto>();

            foreach (var incident in incidents)
            {
                IncidentDto incidentDto = incident.IncidentToDto();

                incidentDtos.Add(incidentDto);
            }
            IQueryable<IncidentDto> queryable = incidentDtos.AsQueryable();

            return PagedList<IncidentDto>.ToPagedList(queryable, incidentParameters.PageNumber, incidentParameters.PageSize);
        }

        private IQueryable<Incident> GetIncidentsByCondition(Expression<Func<Incident, bool>> expression)
        {
            return context.Set<Incident>().Where(expression).AsNoTracking();
        }

        private Incident GetIncidentForUpdateById(Guid id)
        {
            Incident incident = context.Incidents.FirstOrDefault(e => e.IncidentId == id);

            return incident;
        }

        public IncidentDto UpdateIncident(Guid IncidentId, IncidentDto incidentDto)
        {
            var oldIncident = GetIncidentForUpdateById(IncidentId);

            if (oldIncident == null)
            {
                CreateIncident(incidentDto, oldIncident.UserId);
                return incidentDto;
            }
            else
            {
                Incident incident = incidentDto.DtoToIncident();

                oldIncident.Number = incidentDto.Number;
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

                incidentValidator.ValidateAndThrow(incidentDto);

                SaveChanges();

                return oldIncident.IncidentToDto();
            }
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

    }
}
