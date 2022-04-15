import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, filter, Observable, skip, timer } from "rxjs";
import { Incident } from "../../../models/incident.model";
import { environment } from "src/environments/environment";


@Injectable()
export class IncidentService {
    //private readonly _incidents$: BehaviorSubject<Incident>[]

    private GATEWAY_URL = environment.gateway;

    constructor(private httpClient: HttpClient) {

        //this._incidents$ = new BehaviorSubject<Incident[]>([])
    }

    // public getIncidents(): Observable<Partial<Incident>[]> {
    //     timer(3000).subscribe(() => {
    //         this._incidents$.next([
    //             {incidentId: 'string'}
    //         ])
    //     })
        
    //     return this._incidents$.pipe(skip(1))
    // }

    public getIncidents(): Observable<Incident[]>{

        
        return this.httpClient.get<Incident[]>(`${this.GATEWAY_URL}/gateway/get/incidents`);
    
    }

    public getIncidentById(incidentId: string) : Observable<Incident> {

        return this.httpClient.get<Incident>(`${this.GATEWAY_URL}/gateway/get/incidents/${incidentId}`);
        
    }

    public createIncident(incident: Incident): Observable<Incident> {
        return this.httpClient.post<Incident>(`${this.GATEWAY_URL}/gateway/post/incidents`, incident);
    }

    public updateIncident(incident: Incident, incidentId: string): Observable<Incident>{
        return this.httpClient.put<Incident>(`${this.GATEWAY_URL}/gateway/put/incidents`, incident);

    }

    public deleteIncident(incidentId: string): Observable<Incident> {
        return this.httpClient.delete<Incident>(`${this.GATEWAY_URL}/gateway/put/incidents/${incidentId}`);
    }
}