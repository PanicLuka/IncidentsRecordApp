import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { UserService } from '../modules/shared/services/user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private auth: UserService, private router: Router){

  }
  canActivate(): boolean {
    if(this.auth.getToken()==''){
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }
  
}
