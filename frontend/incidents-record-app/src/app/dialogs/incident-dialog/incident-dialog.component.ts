import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { Category } from 'src/app/models/category.model';
import { Incident } from 'src/app/models/incident.model';
import { IncidentService } from 'src/app/modules/shared';
import { CategoryService } from 'src/app/modules/shared/services/category.service';

@Component({
  selector: 'app-incident-dialog',
  templateUrl: './incident-dialog.component.html',
  styleUrls: ['./incident-dialog.component.scss']
})
export class IncidentDialogComponent implements OnInit {
  public get incidentBindingObject(): Partial<Incident> {
    return this._incident
  }



  categorySubscription!: Subscription
  categories!: Category[];
  public flag!: number;
  private _incident: Partial<Incident> = {}


  constructor(public snackBar: MatSnackBar, public dialogRef: MatDialogRef<IncidentDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { incident?: Incident; dialogMode: number },
    public incidentService: IncidentService,
    public categoryService: CategoryService
  ) {
    this.flag = this.data.dialogMode

    if (!!this.data.incident) {
      this._incident = { ...this.data.incident }
    }

  }



  ngOnInit(): void {
    this.categorySubscription = this.incidentService.getCategories()
      .subscribe(data => {
        this.categories = data
      }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
      }

    //console.log();
    // .subscribe(categories => {
    //   this.categories = categories
    // }),
    // (error: Error) => {
    //   console.log(error.name + ' ' + error.message);

    // }

  }

  ngOnDestroy(): void {
    this.categorySubscription.unsubscribe;
  }

  compareTo(a: any, b: any) {
    return a.categoryId == b.categoryId;
  }

  public add(): void {
    this.incidentService.createIncident(this._incident as Incident)
      .subscribe(data => {
        this.snackBar.open('Successfully added incident: ' + this.data.incident?.description, 'Okay', {
          duration: 2500
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
    debugger
    this.incidentService.updateIncident(this._incident.incidentId as string, this._incident as Incident)
      .subscribe(data => {
        this.snackBar.open('Successfully updated auction' + data.incidentId, 'Okay', {
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
    this.incidentService.deleteIncident(this._incident.incidentId as string)
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
