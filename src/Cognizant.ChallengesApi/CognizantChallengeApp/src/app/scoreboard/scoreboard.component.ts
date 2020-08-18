import { Component, Inject } from '@angular/core';
import { Player } from '../shared/player.model';
import { HttpClient } from '@angular/common/http';

import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-scoreboard',
  templateUrl: './scoreboard.component.html',
})
export class ScoreboardComponent {

  displayedColumns: string[] = ['name', 'successSolutions', 'tasks'];
  players: Player[];

  constructor(
    http: HttpClient) {

    http.get<Player[]>(environment.baseUrl + 'api/v1/CognizantChallenges/players')
      .subscribe(result => {
        const descResult = result.sort(function (a, b): any {
          return b.successSolutions - a.successSolutions;
        });

        this.players = descResult;
    }, error => console.error(error));
  }

}
