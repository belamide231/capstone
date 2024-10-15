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
	public remember: boolean = false;


	constructor(private readonly service: RegisterService) {}


	ngOnInit(): void {
		this.service.updatedPhase.subscribe(value => this.phase = value);
		this.service.updatedLoad.subscribe(load => this.load = load);
		this.service.updatedMessage.subscribe(value => this.message = value);
	}


	showIconSwitch() {
		this.show = !this.show;
		console.log(this.show);
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
				await this.service.CreateAccountAsync(this.email, this.password, true);
				break;
		}

	}
}
