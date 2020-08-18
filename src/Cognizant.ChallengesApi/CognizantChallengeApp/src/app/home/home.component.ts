import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Challenge } from '../shared/challenge.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  submitForm: FormGroup;
  taskDescription = '';
  challenges: Challenge[];
  isLoading = false;
  isCorrect: boolean;
  error = '';

  startingCode =
 `using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;

	namespace Rextester
	{
		public class Program
		{
			public static void Main(string[] args)
			{
				string input=Console.ReadLine();
				Console.Write("{0}",input);
			}
		}
	}    `;

  ngOnInit(): void {
    this.submitForm = this.formBuilder.group({
      nickname: ['', Validators.required],
      challenge: ['', Validators.required],
      solution: ['', Validators.required],
    });
    this.submitForm.controls['solution'].setValue(this.startingCode);
  }

  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {
    http.get<Challenge[]>(baseUrl + 'api/v1/CognizantChallenges/challenges').subscribe(result => {
      this.challenges = result;
    }, error => console.error(error));
  }

  onSendSolution(): boolean {
    this.isCorrect = null;
    this.error = null;

    if (this.submitForm.value.nickname === ''
      || this.submitForm.value.challenge === ''
      || this.submitForm.value.solution === '') {
      return;
    }

    this.isLoading = true;

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'name': this.submitForm.value.nickname
      })
    };

    this.http.post<boolean>(this.baseUrl + 'api/v1/CognizantChallenges/' + this.submitForm.value.challenge, JSON.stringify(this.submitForm.value.solution), httpOptions).subscribe({
      next: result => {
        this.isCorrect = result;
        if (result === true) {
          this.submitForm.controls['solution'].setValue(this.startingCode);
        }
      },
      error: _ => {
        this.error = 'an error occured!';
      }
    }).add(() => {
      this.isLoading = false;
    });

  }

  getDescription(id: string): string {
    if (this.challenges == null) {
      return;
    }

    let challenge = this.challenges.find(x => x.id == id);

    if (challenge != null && challenge.description != null) {
      return challenge.description;
    }
  }

}
