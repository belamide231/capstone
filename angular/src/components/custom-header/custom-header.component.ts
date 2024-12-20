import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Cookie } from '../../helpers/cookie.helper';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { PostLoggedService } from '../../app/pages/post-logged/service/post-logged.service';


@Component({
	selector: 'custom-header',
	standalone: true,
	templateUrl: './custom-header.component.html',
	styleUrl: './custom-header.component.sass',
	imports: [
		CommonModule,
		FormsModule
	]
})
export class CustomHeaderComponent implements OnInit {
	role: string = '';
	search: string = '';

	menu: boolean = false;
	notification: boolean = false;

	constructor(private readonly router: Router, private readonly postLoggedService: PostLoggedService) {}

	ngOnInit(): void {
		this.role = this.postLoggedService.getRole()
	}

	onClearSearch() {
		this.search = "";
	}

	onFocusMenu() {
		if(!this.menu) {
			this.menu = true;
			setTimeout(() => document.getElementById("main-menu")?.focus(), 100);
		}
	}
	onBlurMenu() {
		setTimeout(() => this.menu = false, 100);
	}
	onFocusNotification() {
		if(!this.notification) {
			this.notification = true;
			setTimeout(() => document.getElementById("main-notification")?.focus(), 100);
			console.log(this.notification);
		}
	}
	onBlurNotification() {
		setTimeout(() => this.notification = false, 100);
	}


	onRedirectToUsers() {
		this.router.navigate(["/users"]);
	}


	onLogout(): void {
		Cookie.deleteCookie("token", "/");
		this.router.navigate(["/login"]);
	}


	onRedirectDashboard() {
		if(this.router.url !== "/dashboard")
			this.router.navigate(["/dashboard"]);
	}


	onRedirectHome() {
		window.location.href = "/";
	}
}
