import { Component, Input } from '@angular/core';

@Component({
  selector: 'messenger',
  standalone: true,
  imports: [],
  templateUrl: './messenger.component.html',
  styleUrl: './messenger.component.sass'
})
export class MessengerComponent {

    @Input() actives: any = [
        "20210090",
        "20210091",
        "20219992"
    ]
}
