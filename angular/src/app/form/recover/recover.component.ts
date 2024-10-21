import { Component, OnInit } from '@angular/core';
import { RecoverService } from './recover.service';


@Component({
    selector: 'app-recover',
    templateUrl: './recover.component.html',
    styleUrls: ['./recover.component.sass']
})
export class RecoverComponent implements OnInit {

    public phase: number = 1;
    public message: string = "";
    public loading: boolean = false;


    public trust: boolean = true;
    public email: string = "";
    public code: string = "";
    public password: string = "";


    constructor(private readonly service: RecoverService) {}


    ngOnInit(): void {
        this.service.phase.subscribe(value => this.phase = value);
        this.service.message.subscribe(value => this.message = value);
        this.service.loading.subscribe(value => this.loading = value);
    }


    redirectToLogin() {
        this.service.redirectToLogin();
    }


    onSvgClicked() {
        this.phase = 1;
        this.email = "";
        this.password = "";
        this.code = "";
        this.message = "";
    }


    onSubmit() {
        
        this.loading = true;

        setTimeout(() => {
            switch(this.phase) {

                case 1:
                    this.service.VerifyEmailForRecoveryAsync(this.email);
                    break;
    
                case 2:
                    this.service.VerifyCodeForRecoveryAsync(this.email, this.code);
                    break;
    
                case 3:
    
                const lower = "abcdefghijklmnopqrstuvwxyz";
                const capital = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                const numerical = "0123456789";
                const nonAlphaNumerical = "!@#$%^&*()_+[]{}|;:',.<>?/~`";
                
                if (!this.password.split("").some(character => lower.includes(character))) {
                    this.message = "Password requires at least one lowercase letter";
                    this.loading = false;
                } else if (!this.password.split("").some(character => capital.includes(character))) {
                    this.message = "Password requires at least one uppercase letter";
                    this.loading = false;
                } else if (!this.password.split("").some(character => numerical.includes(character))) {
                    this.message = "Password requires at least one numeric character";
                    this.loading = false;
                } else if (!this.password.split("").some(character => nonAlphaNumerical.includes(character))) {
                    this.message = "Password requires at least one non-alphanumeric character";
                    this.loading = false;
                } else if (this.password.length < 8) {
                    this.message = "Password requires at least 8 characters";
                    this.loading = false;
                } else {
                  this.service.NewPasswordForRecoveryAsync(this.email, this.password, this.trust);
                }
                break;
            }
        }, 3000);
    }
}
