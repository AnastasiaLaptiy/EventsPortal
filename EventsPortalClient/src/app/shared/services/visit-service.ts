import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Configuration } from '../config/configuration';
import { Visit } from '../models/visit-model';

@Injectable({
    providedIn: 'root'
})

export class VisitService {
    VisitorsList: Visit[];
    IsEventUserCreate: boolean;

    constructor(private http: HttpClient) { }

    CreateVisit(visit: Visit) {
        return this.http.post(Configuration.URI + '/Visit/CreateVisit', visit);
    }

    GetVisitorsList(eventId: number) {
        return this.http.get(Configuration.URI + '/Visit/GetVisitorsList/' + eventId)
            .toPromise()
            .then(res => this.VisitorsList = res as Visit[]);
    }

    IsEventUserCreated(eventId: number, userId: string) {
        return this.http.get(Configuration.URI + '/Visit/IsEventUserCreated/' + eventId + '/' + userId)
        .toPromise()
        .then(res=> this.IsEventUserCreate = res as boolean);
    }
}
