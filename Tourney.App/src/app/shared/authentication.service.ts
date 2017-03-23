import { Injectable, EventEmitter } from '@angular/core';
import {UserManager, Log, User} from 'oidc-client';

@Injectable()
export class AuthenticationService {

  config : Oidc.UserManagerSettings =  {
    authority: "http://localhost:5000",
    client_id: "js",
    redirect_uri: "http://localhost:5003/callback",
    response_type: "id_token token",
    scope:"openid profile tournaments",
    post_logout_redirect_uri : "http://localhost:5003/index.html",

    // silent_redirect_uri: 'http://localhost:5003',
    // automaticSilentRenew: true,
    // //silentRequestTimeout:10000,

    // filterProtocolClaims: true,
    // loadUserInfo: true
  };
  mgr: UserManager;
  userLoadedEvent: EventEmitter<User> = new EventEmitter<User>();
  constructor() {
    this.mgr = new UserManager(this.config);
    Log.logger = console;
   }

  getUser() {
    this.mgr.getUser().then((user) => {
      console.log("got user", user);
      this.userLoadedEvent.emit(user);
    }).catch(function (err) {
      console.log(err);
    });
  }

  log = function() {
    document.getElementById('results').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('results').innerHTML += msg + '\r\n';
    });
};
 login = () => {
    this.mgr.signinRedirect();
}

 logout = () => {
    this.mgr.signoutRedirect();
}
}
