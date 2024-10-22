import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecoverComponent } from './recover.component';
import { FormsModule } from '@angular/forms';
import { PasswordInputComponent } from '../../../components/password-input/password-input.component';
import { TextInputComponent } from '../../../components/text-input/text-input.component';
import { CodeInputComponent } from '../../../components/code-input/code-input.component';
import { CheckboxInputComponent } from '../../../components/checkbox-input/checkbox-input.component';


@NgModule({
    declarations: [
        RecoverComponent,
        PasswordInputComponent,
        TextInputComponent,
        CodeInputComponent,
        CheckboxInputComponent
    ],
    imports: [
        CommonModule,
        FormsModule
    ],
    exports: [
        RecoverComponent
    ]
})
export class RecoverModule { }
