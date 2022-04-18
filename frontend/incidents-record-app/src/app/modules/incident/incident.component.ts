import { Time } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { IncidentDialogComponent } from 'src/app/dialogs/incident-dialog/incident-dialog.component';
import { Incident } from 'src/app/models/incident.model';
import { IncidentService } from '../shared';

@Component({
  selector: 'app-incident',
  templateUrl: './incident.component.html',
  styleUrls: ['./incident.component.scss']
})
export class IncidentComponent implements OnInit, OnDestroy {

  displayedColumns = ['designation', 'significance', 'workspace', 'date', 'time', 'description', 'thirdPartyHelp',
'problemSolved', 'furtherAction', 'furtherActionPerson', 'actionDescription', 'solvingDate', 'remarks', 'verifies',
'reportedBy', 'actions']

  selectedIncident!: Incident;
  incidentSubscription!: Subscription;
  dataSource!: MatTableDataSource<Incident>;

  constructor(public incidentService: IncidentService, public dialog: MatDialog
    
  ) { }
  

  ngOnInit(): void {
    // this._incidentService
    //   .getIncidents()
    //   .subscribe((incidents) => {
    //     console.log(incidents)
    //   })
    this.loadData();
  }

  ngOnDestroy(): void {
    this.incidentSubscription.unsubscribe();
  }

  loadData() {
    this.incidentSubscription = this.incidentService.getIncidents()
    .subscribe((data) => {
      this.dataSource = new MatTableDataSource(data);
    }),
    (error: Error) => {
      console.log(error.name + ' ' + error.message);
      
    }
    

  }

  public openDialog(flag: number, incidentId?: string, designation?: string , significance?: number, date?: Date , 
    time?: Time, workspace?: string, description?: string, thirdPartyHelp?: string, 
    verifies?: boolean, reportedBy?: string, problemSolved?: boolean, furtherAction?: boolean, 
    furtherActionPerson?: string, actionDescription?: string, solvingDate?: Date, remarks?: string, categoryId?: string)
  {
    const dialogRef = this.dialog.open(IncidentDialogComponent,
    {data: {flag, incidentId, designation , significance, date , time, workspace, description, thirdPartyHelp, verifies, reportedBy, problemSolved, furtherAction, furtherActionPerson, actionDescription, solvingDate, remarks, categoryId}});

    dialogRef.afterClosed().subscribe(result => {
      if(result === 1)
      {
        this.loadData();
      }
    })

  }

  
}
