import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MatTableModule, MatInputModule, MatSelectModule } from '@angular/material';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { HomeComponent } from './home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [HomeComponent],
      imports: [
        MatTableModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        MatInputModule,
        MatSelectModule,
        BrowserAnimationsModule]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it(`form input should exist`, async(() => {
    const form = fixture.debugElement.query(By.css('form'));
    expect(form).toBeTruthy();

    const formField = fixture.debugElement.query(By.css('mat-form-field'));
    expect(formField).toBeTruthy();
  }));

  it(`text input should not be empty`, async(() => {
      expect(component.submitForm.value.solution).toContain('Console');
  }));

});
