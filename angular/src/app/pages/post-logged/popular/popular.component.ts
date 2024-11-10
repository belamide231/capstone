import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FeedsComponentComponent } from "../../../../components/feeds-component/feeds-component.component";
import { RequestPostComponent } from "../../../../components/request-post/request-post.component";
import { ActivitiesFeedsComponent } from "../../../../components/activities-feeds/activities-feeds.component";
import { PostLoggedService } from '../service/post-logged.service';

@Component({
    standalone: true,
    imports: [CommonModule, FeedsComponentComponent, RequestPostComponent, ActivitiesFeedsComponent],
    templateUrl: './popular.component.html',
    styleUrl: './popular.component.sass'
})
export class PopularComponent implements OnInit {

    role: string = '';

    constructor(private readonly postLoggedService: PostLoggedService) {} 

    ngOnInit(): void {
        this.role = this.postLoggedService.getRole();
    }
}
