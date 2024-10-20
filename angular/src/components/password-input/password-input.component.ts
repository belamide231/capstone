import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'password-input',
  templateUrl: './password-input.component.html',
  styleUrl: './password-input.component.sass'
})
export class PasswordInputComponent {


    @Input() password: string = "";
    @Output() passwordChange = new EventEmitter<string>();
    onPasswordChange(newPassword: string) {
        this.passwordChange.emit(newPassword);
    }


    hidden: boolean = false;
    onHiddenChange() {
        this.hidden = !this.hidden;
    } 


    @Input() readonly: string = "off";
}
