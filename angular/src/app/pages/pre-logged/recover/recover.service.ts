import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { RecoverDTO } from './recover.dto';
import { api } from '../../../../helpers/api.helper';
import { Cookie } from '../../../../helpers/cookie.helper';

@Injectable({
  providedIn: 'root'
})
export class RecoverService {


    private setMessage = new BehaviorSubject<string>("");
    private setPhase = new BehaviorSubject<number>(1);
    private setLoading = new BehaviorSubject<boolean>(false); 


    public message = this.setMessage.asObservable();
    public phase = this.setPhase.asObservable();
    public loading = this.setLoading.asObservable();


    constructor(private readonly router: Router) {}


    public redirectToLogin() {
        this.router.navigate(["/login"]);
    }


    public async VerifyEmailForRecoveryAsync(email: string) {

        const endpoint = "api/user/recover/verifySyncedEmail";
        const body = new RecoverDTO.VerifyEmailRecoveryDTO(email);

        try {

            const result = await api.post(endpoint, body);

            switch(result.data.status) {

                case 200:
                    this.setMessage.next("We sent already a code to your email");
                    break;

                case 202:
                    this.setMessage.next("We sent a code to your email");
                    break;

            }


            this.setPhase.next(2);


        } catch (error: any) {

            switch(error.response.data.status) {

                case 404:
                    this.setMessage.next("Email did not exist");
                    break;

                case 423:
                    this.setMessage.next("Email is temporarily locked");
                    break;

                case 500:
                    this.setMessage.next("Something went wrong to the server");
                    break;
            }

        } finally {

            this.setLoading.next(false);
        }
    }


    public async VerifyCodeForRecoveryAsync(email: string, code: string) {

        const endpoint = "api/user/recover/verifyCode";
        const body = new RecoverDTO.VerifyCodeRecoveryDTO(email, code);

        try {

            await api.post(endpoint, body);

            this.setPhase.next(3);
            this.setMessage.next("Enter new password");

        } catch (error: any) {

            switch(error.response.data.status) {

                case 419:
                    this.setMessage.next("Code expired");
                    this.setPhase.next(1);
                    break;
                    
                case 409:
                    this.setMessage.next("Incorrect code");
                    break;

                case 423:
                    this.setMessage.next("Email is temporarily locked");
                    break;

                case 500:
                    this.setMessage.next("Something went wrong to the server");
                    break;
            }

        } finally {
            this.setLoading.next(false);
        }
    }


    public async NewPasswordForRecoveryAsync(email: string, password: string, trust: boolean) {

        const endpoint = "api/user/recover/newPassword";
        const body = new RecoverDTO.NewPassword(email, password, trust);

        try {

            const result = await api.post(endpoint, body);

            console.log(result.data);

            if(result.data.status === 202) {
                
                const deviceId = Cookie.getCookie("deviceId");
                const deviceIdIdentifier = Cookie.getCookie("deviceIdIdentifier");

                if(deviceIdIdentifier === "" || deviceId === "") {              

                    document.cookie = `deviceIdIdentifier=${result.data.deviceIdIdentifier}; path=/;`;
                    document.cookie = `deviceId=${result.data.deviceId}; path=/;`;
                }
            }

            this.setPhase.next(4);
            this.setMessage.next("Password successfully changed");

        } catch (error: any) {

            if(error.response.data.status === 302)
                this.setMessage.next("New password cannot be the same as old");
            else
                this.setMessage.next("Something went wrong to the server");

        } finally {

            this.setLoading.next(false);
        }

    }
}
