import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'post-request',
    standalone: true,
    imports: [
        CommonModule,
        FormsModule,
    ],
    templateUrl: './post-request.component.html',
    styleUrl: './post-request.component.sass'
})
export class PostRequestComponent {

}
