import { Component, OnInit } from '@angular/core';
import { PostLoggedService } from './post-logged.service';

@Component({
    templateUrl: './post-logged.component.html',
    styleUrls: ['./post-logged.component.sass']
})
export class PostLoggedComponent implements OnInit {
    role: string = '';
    constructor(private readonly service: PostLoggedService) {}

    ngOnInit(): void {
        console.log(this.service.getRole());
        this.role = this.service.getRole();
    }
}