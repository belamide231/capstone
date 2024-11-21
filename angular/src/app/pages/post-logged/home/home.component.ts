import { Component, OnInit } from '@angular/core';
import { PostComponentComponent } from '../../../../components/post-component/post-component.component';
import { FeedsComponentComponent } from "../../../../components/feeds-component/feeds-component.component";
import { ActivitiesFeedsComponent } from "../../../../components/activities-feeds/activities-feeds.component";
import { CommonModule } from '@angular/common';
import { PostLoggedService } from '../service/post-logged.service';
import { RequestPostComponent } from "../../../../components/request-post/request-post.component";
import { HomeService } from './home.service';

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
    
    description: string = '';

    arrayOfPost: any[] = [];
    arrayOfPendingPost: any[] = [];

    constructor(private readonly postLoggedService: PostLoggedService, private readonly service: HomeService) {}

    ngOnInit(): void {
        this.service.getPost();
        this.service.getPendingPost();
        this.role = this.postLoggedService.getRole();

        this.service.setListOfPostInHome.subscribe(value => this.arrayOfPost = value);
    }

    onUpdateDescription(updatedDescription: string): void {
        this.description = updatedDescription;
    }

    onPost() {
        this.service.onPost(this.description);
        this.description = '';
    }
}
