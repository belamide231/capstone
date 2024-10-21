import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Cookie } from '../../helpers/cookie.helper';
import { Router } from '@angular/router';


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
	menu: boolean = false;
	notification: boolean = false;


	@Input() position: string = "";


	constructor(private readonly router: Router) {}


	onFocusMenu() {
		this.menu = true;
		setTimeout(() => document.getElementById("main-menu")?.focus(), 100);

	}
	onBlurMenu() {
		setTimeout(() => this.menu = false, 100);
	}
	onFocusNotification() {
		this.notification = true;
		setTimeout(() => document.getElementById("main-notification")?.focus(), 100);
	}
	onBlurNotification() {
		this.notification = false;
	}


	public onLogout(): void {
		Cookie.deleteCookie("token", "/");
		this.router.navigate(["/login"]);
	}
}
