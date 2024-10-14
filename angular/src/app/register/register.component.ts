import { Component } from '@angular/core';

@Component({
	selector: 'app-register',
	templateUrl: './register.component.html',
	styleUrls: ['./register.component.sass']
})
export class RegisterComponent {


	public phase: number = 1;
	email: string = "";



}
