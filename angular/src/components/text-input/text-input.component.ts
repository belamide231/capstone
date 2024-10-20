import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'text-input',
  templateUrl: './text-input.component.html',
  styleUrl: './text-input.component.sass'
})
export class TextInputComponent {
  
  @Input() value: string = "";
  @Output() valueChange = new EventEmitter<string>();
  onValueChange = (newValue: string) => this.valueChange.emit(newValue); 

  @Input() placeholder: string = "Username";

  @Input() readonly: string = "off";

  @Input() svg: string = "off";
  @Output() svgClicked = new EventEmitter<void>();
  onSvgClicked = () => {
    this.svgClicked.emit();
  }
}
