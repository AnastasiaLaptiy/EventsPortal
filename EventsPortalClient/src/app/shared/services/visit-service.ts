import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseRoute } from '../config/BaseRoute';
import { Visit } from '../models/visit-model';

@Injectable({
    providedIn: 'root'
})

export class VisitService {
    constructor(private http: HttpClient) { }

    CreateVisit(visit: Visit) {
        return this.http.post(BaseRoute.Visit + '/CreateVisit', visit);
    }

    GetVisitorsPerEvent(eventId: number) {
        return this.http.get(BaseRoute.Visit + '/GetVisitorsPerEvent/' + eventId);
    }

    GetEnrollEvents() {
        return this.http.get(BaseRoute.Visit + '/GetEnrollEvents');
    }

    GetConfirmedVisits() {
        return this.http.get(BaseRoute.Visit + '/GetConfirmedVisits');
    }
}
