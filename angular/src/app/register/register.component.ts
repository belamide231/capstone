import { Component, OnInit } from '@angular/core';
import { RegisterService } from './register.service';


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
	public trust: boolean = true;


	constructor(private readonly service: RegisterService) {}


	ngOnInit(): void {

		this.service.updatedMessage.subscribe(value => this.message = value);
		this.service.updatedPhase.subscribe(value => this.phase = value);
		this.service.updatedCode.subscribe(value => this.code = value);
		this.service.updatedLoad.subscribe(value => this.load = value);
		this.phase = 1;
	}


	showIconSwitch(): void {

		this.show = !this.show;
	}


	resetIconSwitch(): void {

		this.phase = 1;
		this.email = "";
		this.password = "";
		this.code = "";
		this.message = "";
	}


	public redirectToLogin(): void {

		this.service.redirectToLogin();
	}


	ngSubmit(): void {


		this.load = true;


		setTimeout(() => {


			switch(this.phase) {


				case 1:
	
					this.service.VerifyEmailAsync(this.email);
					break;
	
				case 2:
	
					this.service.UpdateEmailAsync(this.email, this.code);
					break;
	
				case 3:
	
					this.service.CreateAccountAsync(this.email, this.password, this.trust);
					break;
			}
		}, 3000);
	}
}
