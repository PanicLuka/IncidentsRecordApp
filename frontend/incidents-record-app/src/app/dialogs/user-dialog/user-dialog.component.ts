import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Data } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { UserService } from 'src/app/modules/shared/services/user.service';

@Component({
  selector: 'app-user-dialog',
  templateUrl: './user-dialog.component.html',
  styleUrls: ['./user-dialog.component.scss']
})
export class UserDialogComponent implements OnInit {
  public get userBindingObject(): Partial<User> {
    return this._user
  }

  public flag!: number;

  private _user: Partial<User> = {}

  constructor(public snackBar: MatSnackBar, public dialogRef: MatDialogRef<UserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { user?: User; dialogMode: number },
    public userService: UserService,
  ) {
    this.flag = this.data.dialogMode

    if (!!this.data.user) {
      this._user = { ...this.data.user }
    }
  }

  ngOnInit(): void {
    console.log();
  }

  public add(): void {
    this.userService.createUser(this._user as User)
      .subscribe(data => {
        this.snackBar.open('Successfully added user: ' + this.data.user?.userId, 'Okay', {
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
    this.userService.updateUser(this._user.userId as string, this._user as User)
      .subscribe(data => {
        this.snackBar.open('Successfully updated auction' + data.userId, 'Okay', {
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
    this.userService.deleteUser(this._user.userId as string)
      .subscribe(data => {
        this.snackBar.open('Successfully deleted user' + data.userId, 'Okay', {
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