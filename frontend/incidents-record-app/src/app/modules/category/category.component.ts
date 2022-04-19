import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { CategoryDialogComponent } from 'src/app/dialogs/category-dialog/category-dialog.component';
import { Category } from 'src/app/models/category.model';
import { CategoryService } from '../shared/services';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss'],
})
export class CategoryComponent implements OnInit {
  pageSize = 5;
  pageNumber = 1;
  showSpinner = false
  categories!: Category[];
  displayedColumns = ['categoryName', 'actions'];
  dataSource!: MatTableDataSource<Category>;
  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;

  private _categoriesCount: number = 0;

  constructor(
    private categoryService: CategoryService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.showSpinner = true
    setTimeout(() => {
      this.showSpinner = false
      this.loadData();
    }, 3500)
  }

  public loadData() {
    /*this.categoryService.getCategoriesCount().subscribe((count: number) => {
      this._categoriesCount = count;

      this.categoryService
        .getCategories(this.pageSize, this.pageNumber)
        .subscribe(
          (categories) => {
            this.categories = categories;
          },
          (err) => {
            console.log(err);
          }
        );
    });*/
    this.categoryService.getCategoriesCount().subscribe((count: number) => {
      this._categoriesCount = count;
    });
    this.categoryService
      .getCategories(this.pageSize, this.pageNumber)
      .subscribe((data) => {
        this.dataSource = new MatTableDataSource(data);
      }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message);
      };
  }

  public openDialog(flag: number, categoryId?: string, categoryName?: string) {
    const dialogRef = this.dialog.open(CategoryDialogComponent, {
      data: { categoryId, categoryName },
    });
    dialogRef.componentInstance.flag = flag;
    dialogRef.afterClosed().subscribe((result) => {
      if (result === 1) {
        this.loadData();
      }
    });
  }

  onPageChange(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.pageNumber = event.pageIndex + 1;
    this.loadData();
  }

  public get categoriesCount(): number {
    return this._categoriesCount;
  }
}
