import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { UserDialogComponent } from 'src/app/dialogs/user-dialog/user-dialog.component';
import { User } from 'src/app/models/user.model';
import { UserService } from '../shared/services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit, OnDestroy {

  displayedColumns = ['firstName', 'lastName', 'email', 'actions']

  private _usersCount: number = 0;
  selectedUser!: User;
  userSubscription!: Subscription;
  dataSource!: MatTableDataSource<User>;
  showSpinner = false;
  pageSize = 5;
  pageNumber = 1;

  @ViewChild(MatPaginator, { static: false }) paginator!: MatPaginator;

  constructor(public userService: UserService, public dialog: MatDialog

  ) { }


  ngOnInit(): void {
    this.showSpinner = true;
    setTimeout(() => {
      this.showSpinner = false
      this.loadData();
    }, 2000)
    this.loadData();
  }

  ngOnDestroy(): void {
    this.userSubscription.unsubscribe();
  }

  loadData() {
    this.userService.getUsersCount().subscribe((count: number) => {
      this._usersCount = count
    });
    this.userSubscription = this.userService.getUsers(this.pageSize, this.pageNumber)
      .subscribe((data) => {
        this.dataSource = new MatTableDataSource(data);
      }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message);

      }
  }

  onPageChange(event: PageEvent) {
    this.pageSize = event.pageSize;
    this.pageNumber = event.pageIndex + 1;
    this.loadData();
  }


  public openDialog(dialogMode: number, user?: User) {

    const dialogConfig: MatDialogConfig<{ user?: User; dialogMode: number }> =
    {
      data: {
        user,
        dialogMode,
      }
    }

    const dialogRef = this.dialog.open(UserDialogComponent, dialogConfig)

    dialogRef.afterClosed().subscribe(result => {
      if (result === 1) {
        this.loadData();
      }
    })

  }

  public get usersCount(): number {
    return this._usersCount;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  
}
