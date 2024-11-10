import { Component, OnInit } from '@angular/core';
import { PostComponentComponent } from '../../../../components/post-component/post-component.component';
import { FeedsComponentComponent } from "../../../../components/feeds-component/feeds-component.component";
import { ActivitiesFeedsComponent } from "../../../../components/activities-feeds/activities-feeds.component";
import { CommonModule } from '@angular/common';
import { PostLoggedService } from '../service/post-logged.service';
import { RequestPostComponent } from "../../../../components/request-post/request-post.component";

@Component({
    standalone: true,
    templateUrl: './home.component.html',
    styleUrl: './home.component.sass',
    imports: [
    CommonModule,
    PostComponentComponent,
    FeedsComponentComponent,
    ActivitiesFeedsComponent,
    RequestPostComponent
]
})
export class HomeComponent implements OnInit {
    
    task: any[] = [];
    role: string = 'student';

    constructor(private readonly postLoggedService: PostLoggedService) {}

    ngOnInit(): void {
        this.role = this.postLoggedService.getRole();
    }
}
