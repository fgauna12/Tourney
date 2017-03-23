import 'bootstrap';

import { TournamentService } from './tournament/tournament.service';
import { AuthenticationService } from './shared/authentication.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NotfoundComponent } from './routing/notfound/notfound.component';
import { RoutingModule } from './routing/routing.module';
import { NewTournamentComponent } from './new-tournament/new-tournament.component';
import { LoginComponent } from './login/login.component';
import { CallbackComponent } from './login/callback/callback.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    DashboardComponent,
    NotfoundComponent,
    NewTournamentComponent,
    LoginComponent,
    CallbackComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RoutingModule,
    NgbModule.forRoot(),
  ],
  providers: [AuthenticationService, TournamentService],
  bootstrap: [AppComponent]
})
export class AppModule { }
