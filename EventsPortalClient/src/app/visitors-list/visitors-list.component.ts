import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

import { Configuration } from '../shared/config/configuration'

import { VisitService } from '../shared/services/visit-service';
import { EventService } from '../shared/services/event-service';

import { EventItem } from '../shared/models/event-model';
import { Visit } from '../shared/models/visit-model';

@Component({
  selector: 'app-visitors-list',
  templateUrl: './visitors-list.component.html',
  styles: [
  ]
})

export class VisitorsListComponent implements OnInit {
  private subscription: Subscription;
  id: number;
  eventView: EventItem = new EventItem();
  tableMode: boolean = true;
  isVisitorsExists: boolean = false;
  pageOfItemsEvent: Array<Visit>;
  visitEvent = [];
  visitItem: Visit[];

  constructor(
    private activateRoute: ActivatedRoute,
    public visitService: VisitService,
    public eventService: EventService,
    public config: Configuration
  ) {
    this.subscription = activateRoute.params.subscribe(params => this.id = params['eventId']);
  }

  ngOnInit(): void {
    this.visitService.GetVisitorsList(this.id).subscribe((res: any) => {
      debugger;
      this.visitItem = res;
      console.log(res)
      this.visitorsCounter(res);
      this.visitEvent = Array(this.visitItem.length).fill(0).map((x, i) => ({ data: this.visitItem[i] }));
    });
  }

  onChangePage(pageOfItemsEvent: Array<any>) {
    this.pageOfItemsEvent = pageOfItemsEvent;
  }

  editEvent(eventItem: EventItem) {
    this.tableMode = false;
    this.eventView = eventItem;
    debugger;
    console.log(this.eventView);
  }

  visitorsCounter(visitors: Visit[]) {
    if (visitors.length == 0) {
      this.isVisitorsExists = false;
    }
    else
      this.isVisitorsExists = true;
  }

  cancel() {
    this.eventView = new EventItem();
    this.tableMode = true;
  }

  save() {
    this.eventService.EditEvent(this.eventView.Id, this.eventView).subscribe(res => { });
    this.tableMode = true;
  }
}
