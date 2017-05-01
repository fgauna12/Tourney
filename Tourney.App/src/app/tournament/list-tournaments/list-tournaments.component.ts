import { Component, OnInit } from '@angular/core';
import { TournamentService } from "app/tournament/tournament.service";
import { Tournament } from "app/tournament/tournament.model";

@Component({
  selector: 'app-list-tournaments',
  templateUrl: './list-tournaments.component.html',
  styleUrls: ['./list-tournaments.component.scss']
})
export class ListTournamentsComponent implements OnInit {
  total: number;
  tournaments: Tournament[];
  constructor(private tournamentService: TournamentService) { }

  ngOnInit() {
    this.tournamentService.getTournaments().subscribe(response => {
      this.total = response.total;
      this.tournaments = response.results;
    });
  }

}
