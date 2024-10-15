import { Component, OnInit } from '@angular/core';
import { RegisterService } from './register.service';
import { Router } from '@angular/router';


@Component({
	selector: 'app-register',
	templateUrl: './register.component.html',
	styleUrls: ['./register.component.sass']
})
export class RegisterComponent implements OnInit {


	public phase: number = 1;
	public load: boolean = false;
	public show: boolean = false;	
	public message: string = "";

	public email: string = "";
	public code: string = "";
	public password: string = "";
	public trust: boolean = false;


	constructor(private readonly service: RegisterService, private readonly router: Router) {}


	ngOnInit(): void {
		this.service.updatedPhase.subscribe(value => this.phase = value);
		this.service.updatedLoad.subscribe(load => this.load = load);
		this.service.updatedMessage.subscribe(value => this.message = value);
		console.log(this.phase);
	}


	showIconSwitch() {
		this.show = !this.show;
		console.log(this.show);
	}


	trustSwitch() {
		this.trust = !this.trust;
		console.log(this.trust);
	}


	resetIconSwitch() {
		this.phase = 1;
		this.email = "";
		this.password = "";
		this.code = "";
	}


	redirectToLogin() {
		console.log("SHIT");
		this.router.navigate(["/login"]);
	}


	async ngSubmit() {

		switch(this.phase) {

			case 1:
				await this.service.VerifyEmailAsync(this.email);
				break;

			case 2:
				await this.service.UpdateEmailAsync(this.email, this.code);
				break;

			case 3:
				await this.service.CreateAccountAsync(this.email, this.password, this.trust);
				break;

		}

	}
}
