import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../shared/authentication.service';
import { UserManager, Log } from "oidc-client";

@Component({
  selector: 'app-callback',
  templateUrl: './callback.component.html',
  styleUrls: ['./callback.component.scss']
})
export class CallbackComponent implements OnInit {

  constructor(private router: Router,
            private authenticationService: AuthenticationService,
            private userManager: UserManager) {
  }

  ngOnInit() {
    this.userManager.signinRedirectCallback().then(function (user) {
            if (user == null) {
                console.log('waiting')
            }
            else {
                this.router.navigate(['/dashboard']);
            }
        })
        .catch(function (er) {
            console.error(er);
        });
  }
}
