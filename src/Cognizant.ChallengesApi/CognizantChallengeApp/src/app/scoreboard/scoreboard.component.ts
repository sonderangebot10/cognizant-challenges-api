import { Component, Inject } from '@angular/core';
import { Player } from '../shared/player.model';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-scoreboard',
  templateUrl: './scoreboard.component.html',
})
export class ScoreboardComponent {

  displayedColumns: string[] = ['name', 'successSolutions', 'tasks'];
  players: Player[];

  constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {

    http.get<Player[]>(baseUrl + 'api/v1/CognizantChallenges/players')
      .subscribe(result => {
        let descResult = result.sort(function (a, b): any {
          return b.successSolutions - a.successSolutions
        });

        this.players = descResult;
    }, error => console.error(error));
  }

}
