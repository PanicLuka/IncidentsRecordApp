import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { User } from "../../../models/user.model";
import { Login } from "../../../models/login.model";
import { JwtHelperService } from "@auth0/angular-jwt";


export const JWT_NAME = 'token';

@Injectable()
export class UserService {

    public get token(): string {

        return this.getToken();
    }

    public get currentUserRole(): string {

        return this.getCurrentUserRole();
    }

    private GATEWAY_URL = environment.gateway;

    constructor(private httpClient: HttpClient, private jwtHelper: JwtHelperService) {
    }

    public getUsers(pageSize: number, pageNumber: number): Observable<User[]> {
        return this.httpClient.get<User[]>(`${this.GATEWAY_URL}/gateway/get/users`, {params: {
            pageSize: pageSize,
            pageNumber: pageNumber
        }});
    }

    public getUserById(userId: string): Observable<User> {
        return this.httpClient.get<User>(`${this.GATEWAY_URL}/gateway/get/users/${userId}`);
    }

    public getUsersCount(): Observable<number> {
        return this.httpClient.get<number>(`${environment.gateway}/gateway/get/users/count`)
    }

    public loginUser(login: Login): Observable<any> {
        return this.httpClient.post<Login>(`${this.GATEWAY_URL}/gateway/login/login`, login);
    }
    public createUser(user: User): Observable<User> {
        return this.httpClient.post<User>(`${this.GATEWAY_URL}/gateway/post/users`, user);
    }

    public updateUser(userId: string, user: User): Observable<User> {
        return this.httpClient.put<User>(`${this.GATEWAY_URL}/gateway/put/users/${userId}`, user);
    }

    public deleteUser(userId: string): Observable<User> {
        return this.httpClient.delete<User>(`${this.GATEWAY_URL}/gateway/delete/users/${userId}`);
    }
    
    public getToken(): string {
        const token = localStorage.getItem("JWT_NAME");
        if(!this.jwtHelper.isTokenExpired(token!))
        {
            return token!;
        }
        else
        {
            return '';
        }
      }
    
    public getCurrentUserRole(): string {
        let token = localStorage.getItem('JWT_NAME');
        let decodedJWT = JSON.parse(window.atob(token!.split('.')[1]));

        console.log(decodedJWT);
        console.log('role: ' + decodedJWT['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);

        let role = decodedJWT['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
        return role;
      }
}