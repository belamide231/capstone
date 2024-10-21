import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { RegisterDTO } from './register.dto';
import { api } from '../../../helpers/api.helper';
import { Router } from '@angular/router';


@Injectable({
  	providedIn: 'root'
})
export class RegisterService {


	private code: string = "";


	private setMessage = new BehaviorSubject<string>("");
	private setPhase = new BehaviorSubject<number>(1);
	private setLoad = new BehaviorSubject<boolean>(false);
	private setCode = new BehaviorSubject<string>("");


	public updatedMessage = this.setMessage.asObservable();
	public updatedPhase = this.setPhase.asObservable();
	public updatedLoad = this.setLoad.asObservable();
	public updatedCode = this.setCode.asObservable();


	constructor(private readonly router: Router) {}


	redirectToLogin() {

		this.router.navigate(["/login"]);
	}
	
	
	async VerifyEmailAsync(email: string) {


		const endpoint = "api/user/register/verifyEmail";
		const body = new RegisterDTO.VerifyEmailModel(email);


		try {


			const result = await api.post(endpoint, body);

			this.setMessage.next(result	.data.message);
			this.code = result.data.code;
			this.setPhase.next(2);

		} catch (error: any) {


			if(error.response.status === 400)
				this.setMessage.next(error.response.data.errors.Email[0]);
			else 
				this.setMessage.next(error.response.data.message);

		} finally {

			this.setLoad.next(false);
		}
	}


	async UpdateEmailAsync(email: string, code: string) {


		const endpoint = "api/user/register/updateCode";
		const body = new RegisterDTO.UpdateCodeModel(email, code === this.code);

		try {

			await api.post(endpoint, body);
			this.setPhase.next(3);
			this.setMessage.next("");

		} catch (error: any) {


			switch(error.response.data.status) {

				case 409:

					this.setMessage.next("Incorrect code");
					this.setCode.next("");

					break;

				case 403:

					this.setMessage.next("Email is temporarily locked");

					break;

				case 410:

					this.setPhase.next(1);
					this.setMessage.next("Verification code expire");

					break;
			}


		} finally {

			this.setLoad.next(false);
		}

	}


	async CreateAccountAsync(email: string, password: string, trust: boolean) {


		const endpoint = "api/user/register/createAccount";
		const body = new RegisterDTO.CreateAccountModel(email, password, trust);


		if(trust && (document.cookie.split("deviceId=")[1] !== undefined || document.cookie.split("deviceIdIdentifier")[1] !== undefined)) {

			body.deviceId = document.cookie.split("deviceId=")[1].split(";")[0];
			body.deviceIdIdentifier = document.cookie.split("deviceIdIdentifier=")[1].split(";")[0];
		}
		

		try { 

			const result = await api.post(endpoint, body);
			console.log(result.data);


			if(trust && (document.cookie.split("deviceId=")[1] === undefined || document.cookie.split("deviceIdIdentifier")[1] === undefined)) {
				
				document.cookie = `deviceId=${result.data.deviceInfo.deviceId}`;
				document.cookie = `deviceIdIdentifier=${result.data.deviceInfo.deviceIdIdentifier}`;
			}


			this.setPhase.next(4);
			

		} catch (error: any) {

			this.setMessage.next(error.response.data.message);

		} finally {

			this.setLoad.next(false);
		}
	}
}
