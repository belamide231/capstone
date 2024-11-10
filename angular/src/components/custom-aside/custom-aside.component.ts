import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
    selector: 'custom-aside',
    standalone: true,
    imports: [
        FormsModule,
        CommonModule
    ],
    templateUrl: './custom-aside.component.html',
    styleUrl: './custom-aside.component.sass'
})
export class CustomAsideComponent {
    @Input() role: string = 'user';

    constructor(private readonly route: Router) {}

    onRedirections(redirection: string) {
        this.route.navigate([`/${redirection}`]);
    }
}
