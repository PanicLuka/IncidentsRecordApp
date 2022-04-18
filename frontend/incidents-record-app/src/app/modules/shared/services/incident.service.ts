import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Incident } from "../../../models/incident.model";
import { environment } from "src/environments/environment";


@Injectable()
export class IncidentService {


    constructor(private httpClient: HttpClient) {
    }

    public getIncidents(): Observable<Incident[]>{

        
        return this.httpClient.get<Incident[]>(`${environment.gateway}/gateway/get/incidents`);
    
    }

    public getIncidentById(incidentId: string) : Observable<Incident> {

        return this.httpClient.get<Incident>(`${environment.gateway}/gateway/get/incidents/${incidentId}`);
        
    }

    public createIncident(incident: Incident): Observable<Incident> {
        return this.httpClient.post<Incident>(`${environment.gateway}/gateway/post/incidents`, incident);
    }

    public updateIncident(incident: Incident, incidentId: string): Observable<Incident>{
        return this.httpClient.put<Incident>(`${environment.gateway}/gateway/put/incidents/${incidentId}`, incident);

    }

    public deleteIncident(incidentId: string): Observable<Incident> {
        return this.httpClient.delete<Incident>(`${environment.gateway}/gateway/delete/incidents/${incidentId}`);
    }
}