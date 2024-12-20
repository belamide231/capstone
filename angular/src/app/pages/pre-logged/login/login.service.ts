import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { LoginDTO } from './login.dto';
import { api } from '../../../../helpers/api.helper';

@Injectable({
  providedIn: 'root'
})
export class LoginService {


	public setMessage = new BehaviorSubject<string>("");
	public setPhase = new BehaviorSubject<number>(1);
	public setLoad = new BehaviorSubject<boolean>(false);
	public setLock = new BehaviorSubject<boolean>(false);


	public updateMessage = this.setMessage.asObservable();
	public updatePhase = this.setPhase.asObservable();
	public updateLoad = this.setLoad.asObservable();
	public updateLock = this.setLock.asObservable();


	constructor(private router: Router) {}


	public resetData() {
		this.setMessage.next('');
		this.setPhase.next(1);
		this.setLoad.next(false);
		this.setLoad.next(false);
	}


	public redirectToRegister() {
		this.router.navigate(["/register"]);
	}


	public redirectToRecovery() {
		this.router.navigate(["/recover"]);
	}

	
	public async VerifyCredentialAsync(username: string, password: string, remember: boolean) {

		const date = new Date((new Date()).setFullYear(3000));


		const endpoint: string = "api/user/login/verifyCredential";
		const body = new LoginDTO.VerifyCredentialDTO(username, password);


		try {


			const result = await api.post(endpoint, body);


			switch(result.data.status) {


				case 200: 


					this.setMessage.next("");
					document.cookie = `token=${result.data.token}; expires=${date.toUTCString()}; path=/;`;
					this.router.navigate(["/"]);


					if(remember) {

						document.cookie = `username=${username}; expires=${date.toUTCString()}; path=/login;`;
						document.cookie = `password=${password}; expires=${date.toUTCString()}; path=/login;`;
						document.cookie = `remember=1; expires=${date.toUTCString()}; path=/login;`;
		
		
					} else {
		
		
						document.cookie = `username=; max-age=0; path=/login;`
						document.cookie = `password=; max-age=0; path=/login;`
						document.cookie = `remember=; max-age=0; path=/login;`
					}
					

					break;

				case 202:

					this.setMessage.next("We sent a code to your email");
					this.setPhase.next(2);

					break;
			}


		} catch (error: any) {


			error.response.data.status === 423 ? this.setLock.next(true) : this.setLoad.next(false);


			switch(error.response.data.status) {


				case 401:

					this.setMessage.next("Username did not exist");

					break;
				case 423:

					this.setMessage.next("Account is temporarily locked");

					break;
				case 409:

					this.setMessage.next("Incorrect password");
					this.setLock.next(true);

					break;										
				case 500:

					this.setMessage.next("Something went wrong to the server");

					break;
			}


		} finally {


			this.setLoad.next(false);
		}
	}


	public async VerifyLoginCodeAsync(username: string, password: string, code: string, trust: boolean, remember: boolean) {


		const date = new Date((new Date()).setFullYear(3000));

		
		const endpoint = "api/user/login/verifyLoginCode";
		const body = new LoginDTO.VerifyLoginCodeDTO(username, code, trust);


		try {


			const response = await api.post(endpoint, body);


			if(response.data.status === 202) {


				document.cookie = `deviceIdIdentifier=${response.data.deviceIdIdentifier}; expires=${date.toUTCString()}; path=/;`;
				document.cookie = `deviceId=${response.data.deviceId}; expires=${date.toUTCString()}; path=/;`;
			}


			if(remember) {


				document.cookie = `username=${username}; expires=${date.toUTCString()}; path=/login;`;
				document.cookie = `password=${password}; expires=${date.toUTCString()}; path=/login;`;
				document.cookie = `remember=1; expires=${date.toUTCString()}; path=/login;`;


			} else {


				document.cookie = `username=; max-age=0; path=/login;`
				document.cookie = `password=; max-age=0; path=/login;`
				document.cookie = `remember=; max-age=0; path=/login;`
			}


			document.cookie = `token=${response.data.token}; expires=${date.toUTCString()}; path=/;`;
			this.router.navigate(["/"]);


		} catch (error: any) {


			switch(error.response.data.status) {


				case 401: 

					this.setPhase.next(1);
					this.setMessage.next("Invalid account");

					break;

				case 419: 

					this.setPhase.next(1);
					this.setMessage.next("Verification code expired");

					break;

				case 423:

					this.setMessage.next("Verifying account is locked, wait till it unlocks");

					break;


				case 409:

					this.setMessage.next("Incorrect code");

					break;
			}

		} finally {


			this.setLoad.next(false);
		}
	}
}
