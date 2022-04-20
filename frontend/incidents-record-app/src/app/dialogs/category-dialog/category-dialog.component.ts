import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { Category } from 'src/app/models/category.model';
import { CategoryService } from 'src/app/modules/shared';


@Component({
  selector: 'app-category-dialog',
  templateUrl: './category-dialog.component.html',
  styleUrls: ['./category-dialog.component.scss']
})
export class CategoryDialogComponent implements OnInit {

  public flag!: number;
  public subscription!: Subscription;

  constructor(public snackBar: MatSnackBar,
              public dialogRef: MatDialogRef<CategoryDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: Category,
              public categoryService: CategoryService) { }

  ngOnInit(): void {
    console.log('category dialog')
  }

  public createCategory(): void {
    this.categoryService.createCategory(this.data)
      .subscribe(() => {
        this.snackBar.open('Category created with name: ' + this.data.categoryName, 'Okay', {
          duration: 2500
        });
      }),
      (error: Error) => {
        console.log(error.name + '-->' + error.message);
        this.snackBar.open('Something went wrong! Try again!', 'Close', {
          duration: 2500
        });
      };
  }

  public updateCategory(): void {
    this.categoryService.updateCategory(this.data, this.data.categoryId)
      .subscribe(() => {
        this.snackBar.open('Category modified to: ' + this.data.categoryName, 'Okay', {
          duration: 2500
        });
      }),
      (error: Error) => {
        console.log(error.name + '-->' + error.message);
        this.snackBar.open('Something went wrong! Try again!', 'Close', {
          duration: 2500
        });
      };
  }

  public deleteCategory(): void {
    this.categoryService.deleteCategory(this.data.categoryId)
      .subscribe(() => {
        this.snackBar.open('Category successfully deleted', 'Okay', {
          duration: 2500
        });
      }),
      (error: Error) => {
        console.log(error.name + '-->' + error.message);
        this.snackBar.open('Something went wrong! Try again!', 'Close', {
          duration: 2500
        });
      };
  }

  public cancel(): void {
    this.dialogRef.close();
    this.snackBar.open('You gave up on changes', 'Okay', {
      duration: 1000
    });
  }

}
