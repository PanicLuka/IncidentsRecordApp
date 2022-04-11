﻿using System;
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
        private readonly DataContext _context;
        private readonly IncidentValidator _incidentValidator = new IncidentValidator();

        public IncidentsService(DataContext context)
        {
            this._context = context;
        }
        public void CreateIncident(IncidentDto incidentDto, Guid userId)
        {
            Incident incident = incidentDto.DtoToIncident();

            incident.UserId = userId;

            _incidentValidator.ValidateAndThrow(incidentDto);

            _context.Add(incident);

            SaveChanges();
        }

        public void DeleteIncident(Guid id)
        {
            var incident = GetIncidentForUpdateById(id);
            _context.Remove(incident);
            SaveChanges();
        }

        public IncidentWithIdDto GetIncidentById(Guid id)
        {
            Incident incident = _context.Incidents.FirstOrDefault(e => e.IncidentId == id);

            IncidentWithIdDto incidentWithIdDto = incident.IncidentToIncidentWithIdDto();

            return incidentWithIdDto;
        }

        public PagedList<IncidentDto> GetIncidents(IncidentOpts incidentOpts)
        {
            
            List<Incident> incidents = _context.Incidents.ToList();


            var filteredIncidents = FilterIncidents(incidents, incidentOpts);

            incidents = filteredIncidents.ToList();

            List<IncidentDto> incidentDtos = new List<IncidentDto>();

            foreach (var incident in incidents)
            {
                IncidentDto incidentDto = incident.IncidentToDto();

                incidentDtos.Add(incidentDto);
            }
            IQueryable<IncidentDto> queryable = incidentDtos.AsQueryable();

            return PagedList<IncidentDto>.ToPagedList(queryable, incidentOpts.PageNumber, incidentOpts.PageSize);
        }

        private IEnumerable<Incident> FilterIncidents(IEnumerable<Incident> incidentList, IncidentOpts incidentOpts)
        {
            if (incidentOpts.FirstDate.HasValue)
            {
                incidentList = incidentList.Where(o => o.Date >= incidentOpts.FirstDate);
            }
            if (incidentOpts.SecondDate.HasValue)
            {
                incidentList = incidentList.Where(o => o.Date <= incidentOpts.SecondDate);
            }
            if (incidentOpts.FirstSolvingDate.HasValue)
            {
                incidentList = incidentList.Where(o => o.SolvingDate >= incidentOpts.FirstSolvingDate);
            }
            if (incidentOpts.SecondSolvingDate.HasValue)
            {
                incidentList = incidentList.Where(o => o.SolvingDate <= incidentOpts.SecondSolvingDate);
            }
            if (incidentOpts.Significance.HasValue)
            {
                incidentList = incidentList.Where(o => o.Significance == incidentOpts.Significance);
            }
            if (incidentOpts.FurtherAction.HasValue)
            {
                incidentList = incidentList.Where(o => o.FurtherAction == incidentOpts.FurtherAction);
            }
            if (incidentOpts.ThirdPartyHelp.HasValue)
            {
                incidentList = incidentList.Where(o => o.FurtherAction == incidentOpts.ThirdPartyHelp);
            }
            if (incidentOpts.ExactDate.HasValue)
            {
                incidentList = incidentList.Where(o => o.Date == incidentOpts.ExactDate);
            }
            if (incidentOpts.ExactSolvingDate.HasValue)
            {
                incidentList = incidentList.Where(o => o.SolvingDate == incidentOpts.ExactSolvingDate);
            }
            return incidentList;
        }

        private IQueryable<Incident> GetIncidentsByCondition(Expression<Func<Incident, bool>> expression)
        {
            return _context.Set<Incident>().Where(expression).AsNoTracking();
        }

        private Incident GetIncidentForUpdateById(Guid id)
        {
            Incident incident = _context.Incidents.FirstOrDefault(e => e.IncidentId == id);

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
