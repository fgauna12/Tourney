import { AuthenticationService } from '../authentication.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'oidc-client';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  isLoggedIn: boolean;
  currentUser: User;
  constructor(private authenticationService: AuthenticationService) {
  }

  ngOnInit() {
    this.authenticationService.isLoggedIn().subscribe(isLoggedIn => {
      this.isLoggedIn = isLoggedIn;
      if (isLoggedIn) {
        this.loadUser();
      }
    });
  }

  loadUser = () => {
    this.authenticationService.getUser().then((user) => {
      console.log('Loaded user', user);
      this.currentUser = user;
    });
  }

  login = () => {
    this.authenticationService.login();
  }

  logout = () => {
    this.authenticationService.logout();
  }
}
