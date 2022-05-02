import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { IncidentDialogComponent } from 'src/app/dialogs/incident-dialog/incident-dialog.component';
import { IncidentUi } from 'src/app/models/incident-ui';
import { Incident } from 'src/app/models/incident.model';
import { IncidentService } from '../shared';

@Component({
  selector: 'app-incident',
  templateUrl: './incident.component.html',
  styleUrls: ['./incident.component.scss'],
})
export class IncidentComponent implements OnInit, OnDestroy {

  pageSize = 5;
  pageNumber = 1;

  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;

  private _incidentsCount: number = 0;

  displayedColumns = ['designation', 'significance', 'workspace', 'date', 'time', 'description', 'thirdPartyHelp',
    'problemSolved', 'furtherAction', 'furtherActionPerson', 'actionDescription', 'solvingDate', 'remarks', 'verifies',
    'reportedBy', 'category', 'actions',]

  selectedIncident!: IncidentUi;
  incidentSubscription!: Subscription;
  dataSource!: MatTableDataSource<IncidentUi>;

  constructor(public incidentService: IncidentService, public dialog: MatDialog

  ) { }

  onPageChange(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.pageNumber = event.pageIndex + 1;
    this.loadData();
  }

  ngOnInit(): void {
    this.loadData();
  }

  ngOnDestroy(): void {
    this.incidentSubscription.unsubscribe();
  }

  loadData() {
    this.incidentService.getIncidentsCount().subscribe((count: number) => {
      this._incidentsCount = count
    })

    this.incidentService.getIncidents(this.pageSize, this.pageNumber)
      .subscribe((data) => {
        this.dataSource = new MatTableDataSource(data);
      })
  }

  public openDialog(dialogMode: number, incident?: Incident) {

    const dialogConfig: MatDialogConfig<{ incident?: Incident; dialogMode: number }> =
    {
      data: {
        incident,
        dialogMode,
      }
    }


    const dialogRef = this.dialog.open(IncidentDialogComponent, dialogConfig)

    dialogRef.afterClosed().subscribe(result => {
      if (result === 1) {

        this.loadData();
      }
    })

  }

  public get incidentsCount(): number {
    return this._incidentsCount;
  }


}
