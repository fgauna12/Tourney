import { AuthenticationService } from '../shared/authentication.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  constructor(private authenticationService: AuthenticationService) {
  }

  ngOnInit = () => {
    
  }

  login = () => {
    this.authenticationService.login();
  }

  logout = () => {
    this.authenticationService.logout();
  }

 api = () => {
    this.authenticationService.getUser();

    let userLoadedEvent = this.authenticationService.userLoadedEvent;
    userLoadedEvent.subscribe(user => {
      let url = "http://localhost:5001/api/tournaments";

        let xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            console.log(xhr.status, JSON.parse(xhr.responseText));
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();

        userLoadedEvent.unsubscribe();
    });


}

}