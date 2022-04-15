import { Component, OnInit } from '@angular/core';
import { IncidentService } from '../shared';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit {

  constructor( private incidentService : IncidentService ) { }

  ngOnInit(): void {
    this.incidentService
      .getIncidents()
      .subscribe((incidents) => {
        console.log(incidents)
      })

  }

}
