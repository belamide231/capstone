import { Component, OnInit } from '@angular/core';
import { LoginService } from './login.service';
import { Router } from '@angular/router';


@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {

    public phase: number = 1;
    public show: boolean = false;
    public load: boolean = false;
    public message: string = "";
    

    public username: string = "";
    public password: string = "";
    public remember: boolean = false;


    constructor(private readonly service: LoginService, private readonly router: Router) {}


    ngOnInit(): void {
        this.service.updateLoad.subscribe(value => this.load = value);
        this.service.updateMessage.subscribe(value => this.message = value);
        this.service.updatePhase.subscribe(value => this.phase = value);
    }


    public showIconSwitch(): void {
        this.show = !this.show;
    }


    public redirectToRegister(): void {
        this.router.navigate(["/register"]);
    }

    
    ngSubmit() {
        
        switch(this.phase) {
            case 1:
                this.service.VerifyCredentialAsync(this.username, this.password);
                break;

            case 2:
                break;

        }
    }
}
