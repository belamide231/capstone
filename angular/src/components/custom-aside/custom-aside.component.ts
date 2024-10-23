import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'custom-aside',
    standalone: true,
    imports: [],
    templateUrl: './custom-aside.component.html',
    styleUrl: './custom-aside.component.sass'
})
export class CustomAsideComponent {

    constructor(private readonly route: Router) {}

    onRedirectToHome() {
        this.route.navigate(["/"]);
    }
}
