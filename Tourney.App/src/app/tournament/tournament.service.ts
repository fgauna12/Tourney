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
      this.getAuthTokenPromise().then((accessToken) => {
        this.accessToken = accessToken;
      })
      .catch(err => {
        console.error('Could not get auth token', err);
      });
  }

  getTournaments = () : Observable<PagedResponse<Tournament>> => {
    return this.http
      .get('http://localhost:5001/api/tournaments', this.createAuthorizationHeader(this.accessToken))
      .map(response => this.extractData<PagedResponse<Tournament>>(response));
  }

  private extractData<T>(res: Response) {
    if (res.status < 200 || res.status >= 300) {
      throw new Error('Bad response status: ' + res.status);
    }
    let body = res.json ? res.json() : null;
    return <T>(body && body.data || {});
  }

  private getAuthTokenPromise = () : Promise<string> => {
    return this.authenticationService.getUser().then((user) => {
      return user.access_token;
    });
  }

  private getRequestOptions = (headers: Headers) : RequestOptionsArgs => {
    return {
      headers : headers
    };
  }

  private createAuthorizationHeader(accessToken: string): RequestOptionsArgs {
    const headers = new Headers();
    headers.append('Authorization',  `Bearer ${accessToken}`);
    const requestOptions: RequestOptionsArgs = {
      headers : headers
    };
    return requestOptions;
  }
}
