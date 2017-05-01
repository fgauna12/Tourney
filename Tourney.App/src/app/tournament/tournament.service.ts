import { RequestOptionsArgs } from '@angular/http/src/interfaces';
import { AuthenticationService } from '../shared/authentication.service';
import { Headers, Http, Response } from '@angular/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { Tournament, PagedResponse } from './tournament.model';

@Injectable()
export class TournamentService {
  accessToken: string;
  constructor(private http: Http, private authenticationService: AuthenticationService) {
  }

  getTournaments = () : Observable<PagedResponse<Tournament>> => {
    return this.http
      .get('http://localhost:5001/api/tournaments')
      .map(response => response.json());
  }
}
