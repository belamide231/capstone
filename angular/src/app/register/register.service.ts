import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { RegisterModels } from './register.models';
import { api } from '../api';


@Injectable({
  	providedIn: 'root'
})
export class RegisterService {


	private code: string = "";


	private setLoad = new BehaviorSubject<boolean>(false);
	private setPhase = new BehaviorSubject<number>(3);
	private setMessage = new BehaviorSubject<string>("");


	public updatedLoad = this.setLoad.asObservable();
	public updatedPhase = this.setPhase.asObservable();
	public updatedMessage = this.setMessage.asObservable();
	
	
	async VerifyEmailAsync(email: string) {


		this.setLoad.next(true);

		
		const endpoint = "api/user/register/verifyEmail";
		const body = new RegisterModels.VerifyEmailModel(email);


		try {


			const result = await api.post(endpoint, body);

			this.setMessage.next(result	.data.message);
			this.code = result.data.code;
			this.setPhase.next(2);

		} catch (error: any) {

			console.log(error.response.data.message);

			if(error.response.status === 400)
				this.setMessage.next(error.response.data.errors.Email[0]);
			else 
				this.setMessage.next(error.response.data.message);

		} finally {

			this.setLoad.next(false);
		}
	}


	async UpdateEmailAsync(email: string, code: string) {


		this.setLoad.next(true);
		
		
		const endpoint = "api/user/register/updateCode";
		const body = new RegisterModels.UpdateCodeModel(email, code === this.code);

		try {

			await api.post(endpoint, body);
			this.setPhase.next(3);
			this.setMessage.next("");

		} catch (error: any) {

			console.log(error.response.data);

			if(error.response.data.status === 409) {
				this.setMessage.next("Incorrect code");
			} else if(error.response.data.status === 403) {
				this.setMessage.next("Email is locked");
			} else if(error.response.data.status === 410) {
				this.setPhase.next(1);
				this.setMessage.next("Verification code expire.");
			}

		} finally {

			this.setLoad.next(false);
		}

	}


	async CreateAccountAsync(email: string, password: string, trust: boolean) {


		this.setLoad.next(true);


		const endpoint = "api/user/register/createAccount";
		const body = new RegisterModels.CreateAccountModel(email, password, trust);


		if(trust && (document.cookie.split("DeviceID=")[1] !== undefined || document.cookie.split("DeviceIDIdentifier")[1] !== undefined)) {

			body.deviceId = document.cookie.split("deviceId=")[1].split(";")[0];
			body.deviceIdIdentifier = document.cookie.split("deviceIdIdentifier=")[1].split(";")[0];
		}
		

		try { 

			const result = await api.post(endpoint, body);
			console.log(result.data);

		} catch (error: any) {

			console.log(error.response.data);
			this.setMessage.next(error.response.data.message);

		} finally {

			this.setLoad.next(false);
		}
	}
}
