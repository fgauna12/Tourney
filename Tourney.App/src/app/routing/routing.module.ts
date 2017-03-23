import { CallbackComponent } from '../login/callback/callback.component';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { NotfoundComponent } from './notfound/notfound.component';
import { NewTournamentComponent } from '../new-tournament/new-tournament.component';

const routes: Routes = [
  { path: 'dashboard', component: DashboardComponent },
  { path: 'new-tournament', component: NewTournamentComponent },
  { path: 'callback', component: CallbackComponent },
  { path: '', pathMatch: 'full', redirectTo: '/dashboard' },
  { path: '**', pathMatch: 'full', component: NotfoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class RoutingModule { }
