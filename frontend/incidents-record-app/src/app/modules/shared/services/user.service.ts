import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { User } from "../../../models/user.model";
@Injectable()
export class UserService {

    private GATEWAY_URL = environment.gateway;

    constructor(private httpClient: HttpClient) {
    }

    public getUsers(): Observable<User[]> {
        return this.httpClient.get<User[]>(`${this.GATEWAY_URL}/gateway/get/users`);
    }

    public getUserById(userId: string): Observable<User[]> {
        return this.httpClient.get<User[]>(`${this.GATEWAY_URL}/gateway/get/users/${userId}`);
    }

    public loginUser(user: User): Observable<User[]> {
        return this.httpClient.post<User[]>(`${this.GATEWAY_URL}/gateway/post/login`, user);
    }
    public createUser(user: User): Observable<User> {
        return this.httpClient.post<User>(`${this.GATEWAY_URL}/gateway/post/users`, user);
    }

    public updateUser(user: User): Observable<User> {
        return this.httpClient.put<User>(`${this.GATEWAY_URL}/gateway/put/users`, user);
    }

    public deleteUser(userId: string): Observable<User> {
        return this.httpClient.delete<User>(`${this.GATEWAY_URL}/gateway/delete/users/${userId}`);
    }
}