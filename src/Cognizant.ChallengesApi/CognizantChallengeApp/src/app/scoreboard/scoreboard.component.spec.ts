import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MatTableModule } from '@angular/material';
import { HttpClientModule } from '@angular/common/http';

import { ScoreboardComponent } from './scoreboard.component';
import { By } from '@angular/platform-browser';
import { Player } from '../shared/player.model';

describe('ScoreboardComponent', () => {
  let component: ScoreboardComponent;
  let fixture: ComponentFixture<ScoreboardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ScoreboardComponent],
      imports: [
        MatTableModule,
        HttpClientModule]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScoreboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it(`should have undefined players in the begining`, async(() => {
    expect(component.players).toBeUndefined();
  }));

  it(`should have three table columns`, async(() => {
    expect(component.displayedColumns.length).toEqual(3);
  }));

  it(`table should exist`, async(() => {
    expect(fixture.debugElement.query(By.css('mat-table'))).toBeTruthy();
  }));

  it(`table should be empty`, async(() => {
    const menuDebugElement = fixture.debugElement.query(By.css('mat-cell'));
    expect(menuDebugElement).toBeNull();
  }));

  it(`table should populate`, async(() => {
    expect(component).toBeTruthy();
    const players: Player[] = [
      {
        'name': 'Justas',
        'successSolutions': 1,
        'tasks': ['Fibonacci']
      }
    ];
    component.players = players;

    fixture.detectChanges();

    const menuDebugElement = fixture.debugElement.query(By.css('mat-cell'));
    expect(menuDebugElement).toBeTruthy();
  }));
});
