import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';

@Component({
    selector: 'activities-feeds',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './activities-feeds.component.html',
    styleUrl: './activities-feeds.component.sass'
})
export class ActivitiesFeedsComponent implements OnInit {
    tasks: any = [];
    @Input() role: string = '';

    ngOnInit() {
        console.log(this.role);
    }
}
