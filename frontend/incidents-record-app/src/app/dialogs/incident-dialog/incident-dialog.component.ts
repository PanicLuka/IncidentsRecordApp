import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Data } from '@angular/router';
import { Incident } from 'src/app/models/incident.model';
import { IncidentService } from 'src/app/modules/shared';

@Component({
  selector: 'app-incident-dialog',
  templateUrl: './incident-dialog.component.html',
  styleUrls: ['./incident-dialog.component.scss']
})
export class IncidentDialogComponent implements OnInit {

  public flag!: number;

  constructor(public snackBar: MatSnackBar, public dialogRef: MatDialogRef<IncidentDialogComponent>,
    @Inject (MAT_DIALOG_DATA) public data: Incident & {flag: number},
    public incidentService: IncidentService,
    ) {
      this.flag = this.data.flag
     }

  ngOnInit(): void {
  }

  public add(): void {
    this.incidentService.createIncident(this.data)
    .subscribe(data => {
      this.snackBar.open('Successfully added incident: ' + this.data.incidentId, 'Okay', {
        duration:2500
      });
    }),
    (error: Error) => {
      console.log(error.name + '-----> ' + error.message)
      this.snackBar.open('There has been a mistake, try again!', 'Close', {
        duration: 2500
      });
    };
  }

  public update(): void {
    this.incidentService.updateIncident(this.data, this.data.incidentId)
    .subscribe(data => {
      this.snackBar.open('Successfully updated auction' + data.incidentId, 'Okay',{
        duration: 2500
      });
    });
    (error: Error) => {
      console.log(error.name + '-----> ' + error.message)
      this.snackBar.open('There has been a mistake, try again!', 'Close', {
        duration: 2500
      });
    }
  } 

  public delete(): void {
    this.incidentService.deleteIncident(this.data.incidentId)
    .subscribe(data => {
      this.snackBar.open('Successfully deleted incident' + data.incidentId, 'Okay', {
        duration: 2500
      });
    });
    (error: Error) => {
      console.log(error.name + '-----> ' + error.message)
      this.snackBar.open('There has been a mistake, try again!', 'Close', {
        duration: 2500
      });
    }

  }

  public cancel(): void {
    this.dialogRef.close();
    this.snackBar.open('You gave up on changes', 'Okay', {
      duration: 1000
    });
  };

}
