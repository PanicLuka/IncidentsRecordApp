import { Injectable } from "@angular/core";
import { BehaviorSubject, filter, Observable, skip, timer } from "rxjs";
import { Incident } from "../../../models/incident.model";

@Injectable()
export class IncidentService {
    private readonly _incidents$: BehaviorSubject<Partial<Incident>[]>

    constructor() {
        this._incidents$ = new BehaviorSubject<Partial<Incident>[]>([])
    }

    public getIncidents(): Observable<Partial<Incident>[]> {
        timer(3000).subscribe(() => {
            this._incidents$.next([
                {incidentId: 'string'}
            ])
        })
        
        return this._incidents$.pipe(skip(1))
    }
}