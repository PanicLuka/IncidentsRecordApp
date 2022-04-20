import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Incident } from "../../../models/incident.model";

@Injectable()
export class IncidentService {
    constructor(private http: HttpClient) { }

    public getIncidents(): Observable<Incident[]> {
        return this.http.get<Incident[]>(`${environment.gateway}/gateway/get/incidents`)
    }
}