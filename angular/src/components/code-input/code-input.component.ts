import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'code-input',
    templateUrl: './code-input.component.html',
    styleUrl: './code-input.component.sass'
})
export class CodeInputComponent {
    

    @Input() code: string = "";
    @Output() codeChange = new EventEmitter<string>();
    onCodeChange(newCode: string) {
        this.codeChange.emit(newCode);
    }
}
