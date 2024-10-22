import { Component, OnInit } from '@angular/core';
import { LoginService } from './login.service';
import { Cookie } from '../../../helpers/cookie.helper';


@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {


    public phase: number = 1;
    public show: boolean = false;
    public load: boolean = false;
    public lock: boolean = false;
    public message: string = "";
    

    public code: string = "";
    public username: string = "";
    public password: string = "";
    public remember: boolean = false;
    public trust: boolean = true;


    constructor(private readonly service: LoginService) {}


    public ngOnInit(): void {
        this.lock = false;
        this.username = Cookie.getCookie("username");
        this.password = Cookie.getCookie("password");
        this.remember = Cookie.getCookie("remember") === "" ? false : true;
        this.service.updateMessage.subscribe(value => this.message = value);
        this.service.updatePhase.subscribe(value => this.phase = value);
        this.service.updateLock.subscribe(value => this.lock = value);
        this.service.updateLoad.subscribe(value => this.load = value);
        this.phase = 1
    }


    public showIconSwitch(): void {

        this.show = !this.show;
    }


    public redirectToRegister(): void {

        this.service.redirectToRegister();
    }


    public redirectToRecovery(): void {

        this.service.redirectToRecovery();
    }


    public resetFillings(): void {
        
        this.username = "";
        this.password = "";
        this.message = "";
        this.code = "";
        this.lock = false;
        this.phase = 1;
    }

    
    public ngSubmit() {

        this.load = true;
        
        setTimeout(() => {

            switch(this.phase) {
                case 1:
    
                    this.service.VerifyCredentialAsync(this.username, this.password, this.remember);
                    break;
    
                case 2:
    
                    this.service.VerifyLoginCodeAsync(this.username, this.password, this.code, this.trust, this.remember);
                    break;
            }
        }, 3000)
    }
}
