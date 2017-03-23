import { AuthenticationService } from '../shared/authentication.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  loadedUserSub: any;
  currentUser: any;
  constructor(private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.loadedUserSub = this.authenticationService.userLoadedEvent
      .subscribe(user => {
        this.currentUser = user;
      });
  }

  getUser() {
      this.authenticationService.getUser();
    }
}
