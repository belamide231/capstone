import { Injectable } from '@angular/core';
import { api } from '../api';
import { BehaviorSubject } from 'rxjs';
import { LoginModels } from './login.models';


@Injectable({
  providedIn: 'root'
})
export class LoginService {

	private setPhase = new BehaviorSubject<number>(1);
	private setMessage = new BehaviorSubject<string>("");
	private setLoad = new BehaviorSubject<boolean>(false);


	public updateMessage = this.setMessage.asObservable();
	public updateLoad = this.setLoad.asObservable();
	public updatePhase = this.setPhase.asObservable();

	
	public async VerifyCredentialAsync(username: string, password: string) {

		
		this.setLoad.next(true);


		const endpoint: string = "api/user/login/verifyCredential";
		const body = new LoginModels.VerifyCredentialModel(username, password);


		try {


			this.setMessage.next("");
			const result = await api.post(endpoint, body);

			console.log(result.data);

			switch(result.data.status) {

				case 200: 
					break;
				case 202:
					break;
			}

		} catch (error: any) {


			switch(error.response.data.status) {


				case 401:
					this.setMessage.next("Username did not exist");
					break;
				case 423:
					this.setMessage.next("Account is temporarily locked");
					break;
				case 409:
					this.setMessage.next("Incorrect password");
					break;										
				case 500:
					this.setMessage.next("Something went wrong to the server");
					break;
			}


		} finally {


			this.setLoad.next(false);
		}
	}


	public async VerifyLoginCodeAsync(username: string, password: string) {

	}
}
