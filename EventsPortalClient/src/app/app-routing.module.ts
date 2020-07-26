import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PublicEventListComponent } from './public-event-list/public-event-list.component';
import { AuthGuard } from './shared/config/auth-provider';
import { PrivateEventListComponent } from './private-event-list/private-event-list.component';
import { AuthComponent } from './auth/auth.component';


const routes: Routes = [
  { path: '', component: PublicEventListComponent },
  { path: 'private-event-list', component: PrivateEventListComponent, canActivate: [AuthGuard]},
  { path: 'login', component: AuthComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }