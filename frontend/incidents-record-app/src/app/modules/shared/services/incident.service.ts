import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Incident } from "../../../models/incident.model";
import { environment } from "src/environments/environment";
import { Category } from "src/app/models/category.model";


@Injectable()
export class IncidentService {


    constructor(private httpClient: HttpClient) {
    }

    public getIncidents(pagesize: number, pageNumber: number): Observable<Incident[]> {


        return this.httpClient.get<Incident[]>(`${environment.gateway}/gateway/get/incidents`, {
            params: {
                pagesize: pagesize,
                pageNumber: pageNumber
            }
        });

    }

    public getCategories(): Observable<Category[]> {
        return this.httpClient.get<Category[]>(`${environment.gateway}/gateway/get/categories`);
    }


    public getIncidentsCount(): Observable<number> {
        return this.httpClient.get<number>(`${environment.gateway}/gateway/get/incidents/count`)
    }

    public getIncidentById(incidentId: string): Observable<Incident> {

        return this.httpClient.get<Incident>(`${environment.gateway}/gateway/get/incidents/${incidentId}`);

    }

    public createIncident(incident: Incident): Observable<Incident> {

        incident.furtherAction = Boolean(incident.furtherAction);
        incident.thirdPartyHelp = Boolean(incident.thirdPartyHelp);
        incident.significance = Number(incident.significance);


        return this.httpClient.post<Incident>(`${environment.gateway}/gateway/post/incidents`, incident);
    }

    public updateIncident(incidentId: string, incident: Incident): Observable<Incident> {
        return this.httpClient.put<Incident>(`${environment.gateway}/gateway/put/incidents/${incidentId}`, incident);

    }

    public deleteIncident(incidentId: string): Observable<Incident> {
        return this.httpClient.delete<Incident>(`${environment.gateway}/gateway/delete/incidents/${incidentId}`);
    }
}