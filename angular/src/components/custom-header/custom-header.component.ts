import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
	selector: 'custom-header',
	standalone: true,
	templateUrl: './custom-header.component.html',
	styleUrl: './custom-header.component.sass',
	imports: [
		CommonModule
	]
})
export class CustomHeaderComponent {
	@Input() active: boolean = true;


	public activate(): void {
		console.log(this.activate);
		this.active = !this.active;
	}
}
