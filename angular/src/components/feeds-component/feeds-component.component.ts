import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
    selector: 'feeds-component',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './feeds-component.component.html',
    styleUrls: ['./feeds-component.component.sass']
})
export class FeedsComponentComponent {
    posts: any[] = [];
}
