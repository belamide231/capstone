import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
    selector: 'activities-feeds',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './activities-feeds.component.html',
    styleUrl: './activities-feeds.component.sass'
})
export class ActivitiesFeedsComponent  {
    tasks: any = [];
    @Input() role: string = '';
}
