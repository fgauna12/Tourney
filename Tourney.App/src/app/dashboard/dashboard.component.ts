import { Tournament } from '../tournament/tournament.model';
import { TournamentService } from '../tournament/tournament.service';
import { AuthenticationService } from '../shared/authentication.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  tournaments: Tournament[];
  totalTournaments: number;
  constructor(private authenticationService: AuthenticationService,
              private tournamentService: TournamentService) { }

  ngOnInit() {
    this.loadTournaments();
  }

  loadTournaments() {
    this.tournaments = [];
    this.tournamentService.getTournaments()
      .subscribe(pagedResponse => {
        this.tournaments = pagedResponse.results;
        this.totalTournaments = pagedResponse.total;
      },
      err => {
        console.error('Could not get the tournaments');
      });
  }
}
