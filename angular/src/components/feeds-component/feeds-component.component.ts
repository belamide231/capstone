import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PostLoggedService } from '../../app/pages/post-logged/service/post-logged.service';

@Component({
    selector: 'feeds-component',
    standalone: true,
    imports: [
        CommonModule,
        FormsModule,
    ],
    templateUrl: './feeds-component.component.html',
    styleUrls: ['./feeds-component.component.sass']
})
export class FeedsComponentComponent implements OnInit {
    @Input() empty: string = '';
    @Input() posts: any[] = [];
    firstLetterOfTheName: string = '';

    constructor(private readonly postLoggedService: PostLoggedService) {}

    ngOnInit(): void {
        this.firstLetterOfTheName = this.postLoggedService.getEmail()[0].toUpperCase();
    }
}