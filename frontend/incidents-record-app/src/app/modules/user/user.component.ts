import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
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

  displayedColumns = ['firstName', 'lastName', 'email', 'password', 'actions']

  selectedUser!: User;
  userSubscription!: Subscription;
  dataSource!: MatTableDataSource<User>;

  constructor(public userService: UserService, public dialog: MatDialog

  ) { }


  ngOnInit(): void {
    // this._userService
    //   .getUsers()
    //   .subscribe((users) => {
    //     console.log(users)
    //   })
    this.loadData();
  }

  ngOnDestroy(): void {
    this.userSubscription.unsubscribe();
  }

  loadData() {
    this.userSubscription = this.userService.getUsers()
      .subscribe((data) => {
        this.dataSource = new MatTableDataSource(data);
      }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message);

      }


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


}
