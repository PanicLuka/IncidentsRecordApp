import { Component, OnInit } from '@angular/core';
import { IncidentService } from '../shared';

@Component({
  selector: 'app-incident',
  templateUrl: './incident.component.html',
  styleUrls: ['./incident.component.scss']
})
export class IncidentComponent implements OnInit {

  constructor(
    private readonly _incidentService: IncidentService
  ) { }

  ngOnInit(): void {
    this._incidentService
      .getIncidents()
      .subscribe((incidents) => {
        console.log(incidents)
      })
  }

}
