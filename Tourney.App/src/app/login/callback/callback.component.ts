import { Router } from '@angular/router';
import { UserManagerConfiguration } from '../../shared/authentication.service';
import { Component, OnInit } from '@angular/core';
import { UserManager, Log } from "oidc-client";


@Component({
  selector: 'app-callback',
  templateUrl: './callback.component.html',
  styleUrls: ['./callback.component.scss']
})
export class CallbackComponent implements OnInit {

  userManager : UserManager;
  constructor(private router: Router) {
      this.userManager = new UserManager(UserManagerConfiguration);
  }

  ngOnInit() {
    this.userManager.signinRedirectCallback().then((user)  => {
            if (user == null) {
                console.log('waiting')
            }
            else {
                this.router.navigate(['/dashboard']);
            }
        })
        .catch((er) => {
            console.error(er);
        });
  }
}
