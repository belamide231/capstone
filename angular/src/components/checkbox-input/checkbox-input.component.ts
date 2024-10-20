import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
	selector: 'checkbox-input',
	templateUrl: './checkbox-input.component.html',
	styleUrl: './checkbox-input.component.sass'
})
export class CheckboxInputComponent {
	

	@Input() value: boolean = false;
	@Output() valueChange = new EventEmitter<boolean>();
	onValueChange(newValue: boolean) {
		this.valueChange.emit(newValue);
	}


	@Input() disabled: boolean = false;


	@Input() span: string = "";
}
