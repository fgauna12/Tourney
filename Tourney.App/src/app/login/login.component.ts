import { Component, OnInit } from '@angular/core';
import {UserManager, Log} from 'oidc-client';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  config : Oidc.UserManagerSettings =  {
    authority: "http://localhost:5000",
    client_id: "js",
    redirect_uri: "http://localhost:5003/callback.html",
    response_type: "id_token token",
    scope:"openid profile tournaments",
    post_logout_redirect_uri : "http://localhost:5003/index.html",

    silent_redirect_uri: 'http://localhost:5003',
    automaticSilentRenew: true,
    //silentRequestTimeout:10000,

    filterProtocolClaims: true,
    loadUserInfo: true
  };
  mgr: UserManager;
  constructor() {
    this.mgr = new UserManager(this.config);
    Log.logger = console;
   }

  ngOnInit = () => {
    
    this.mgr.getUser().then(function (user) {
        if (user) {
            this.log("User logged in", user.profile);
        }
        else {
            this.log("User not logged in");
        }
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

 api = () => {
    this.mgr.getUser().then(function (user) {
        var url = "http://localhost:5001/api/tournaments";

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            console.log(xhr.status, JSON.parse(xhr.responseText));
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}

 logout = () => {
    this.mgr.signoutRedirect();
}

}