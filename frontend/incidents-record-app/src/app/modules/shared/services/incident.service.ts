import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { forkJoin, mergeMap, Observable, Subject } from "rxjs";
import { Incident } from "../../../models/incident.model";
import { environment } from "src/environments/environment";
import { IncidentUi } from "src/app/models/incident-ui";
import { CategoryService } from "./category.service";


@Injectable()
export class IncidentService {
    constructor(
        private httpClient: HttpClient,
        private categoryService: CategoryService
    ) {
    }

    public getIncidents(pagesize: number, pageNumber: number): Observable<IncidentUi[]> {


        return this.httpClient.get<Incident[]>(`${environment.gateway}/gateway/get/incidents`, {
            params: {
                pagesize: pagesize,
                pageNumber: pageNumber
            }
        }).pipe(
            mergeMap(incidents => {
                const obs$ = []
                for (const { categoryId } of incidents) {
                    const categoryObs$ = this.categoryService.getCategory(categoryId)
                    obs$.push(categoryObs$)
                }
                let incidentsUi: IncidentUi[] = []
                const returnObs$ = new Subject<IncidentUi[]>()

                forkJoin(obs$).subscribe((categories) => {
                    incidentsUi = incidents.map(incident => {

                        const category = categories.find(it => it.categoryId === incident.categoryId)
                        if (category) {
                            const retVal: IncidentUi = {
                                incidentId: incident.incidentId,
                                designation: incident.designation,
                                significance: incident.significance,
                                workspace: incident.workspace,
                                date: incident.date,
                                time: incident.time,
                                description: incident.description,
                                thirdPartyHelp: incident.thirdPartyHelp,
                                problemSolved: incident.problemSolved,
                                furtherAction: incident.furtherAction,
                                furtherActionPerson: incident.furtherActionPerson,
                                actionDescription: incident.actionDescription,
                                solvingDate: incident.solvingDate,
                                remarks: incident.remarks,
                                verifies: incident.verifies,
                                reportedBy: incident.reportedBy,
                                category,
                            }

                            return retVal
                        }
                        else {
                            throw new Error('Cant find incident category for category id: ' + incident.categoryId)
                        }
                    })

                    returnObs$.next(incidentsUi)
                })

                return returnObs$
            })
        )

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