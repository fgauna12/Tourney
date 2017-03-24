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
  currentUserName: string;
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
    this.authenticationService.getUser().then((user: User) => {
      console.log('Loaded user', user);
      this.currentUser = user;
      this.currentUserName = user.profile ? (Array.isArray(user.profile.name) ? user.profile.name[0] : user.profile.name) : null;
    });
  }

  login = () => {
    this.authenticationService.login();
  }

  logout = () => {
    this.authenticationService.logout();
  }
}
