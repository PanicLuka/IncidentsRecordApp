import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { CategoryDialogComponent } from 'src/app/dialogs/category-dialog/category-dialog.component';
import { Category } from 'src/app/models/category.model';
import { Incident } from 'src/app/models/incident.model';
import { CategoryService, IncidentService } from '../shared';

@Component({
  selector: 'app-incident',
  templateUrl: './incident.component.html',
  styleUrls: ['./incident.component.scss']
})
export class IncidentComponent implements OnInit, OnDestroy {

  pageSize = 10;
  pageNumber = 1;
  displayedColumns = ['categoryName', 'actions'];
  dataSource!: MatTableDataSource<Category>;
  categorySubscription!: Subscription;
  @ViewChild(MatSort, {static: false}) sort!: MatSort;
  @ViewChild(MatPaginator, {static: false}) paginator!: MatPaginator;
  
  constructor(private categoryService: CategoryService,
              private dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadData()
  }

  ngOnDestroy(): void {
    this.categorySubscription.unsubscribe();
  }

  public loadData() {
    this.categorySubscription = this.categoryService.getCategories(this.pageSize, this.pageNumber)
    .subscribe(data => {
      console.log(data);
      this.dataSource = new MatTableDataSource(data);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;

    }),
    (error: Error) => {
      console.log(error.name + '  ' + error.message);
    }
  }

  public openDialog(flag: number, categoryId?: string, categoryName?: string) {
    const dialogRef = this.dialog.open(CategoryDialogComponent, {data: { categoryId, categoryName }});
    dialogRef.componentInstance.flag = flag;
    dialogRef.afterClosed()
      .subscribe(result => {
        if(result===1) {
          this.loadData();
        }
      })
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  onPageChange(event: PageEvent) {
    console.log(event)
    const startIndex = event.pageIndex * event.pageSize
    let endIndex = startIndex + event.pageSize
  }
  
}
