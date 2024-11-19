import { Component, OnInit } from '@angular/core';
import { FeedsComponentComponent } from "../../../../components/feeds-component/feeds-component.component";
import { RequestPostComponent } from "../../../../components/request-post/request-post.component";
import { ActivitiesFeedsComponent } from "../../../../components/activities-feeds/activities-feeds.component";
import { PostLoggedService } from '../service/post-logged.service';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-questions',
    standalone: true,
    imports: [CommonModule, FeedsComponentComponent, RequestPostComponent, ActivitiesFeedsComponent],
    templateUrl: './questions.component.html',
    styleUrl: './questions.component.sass'
})
export class QuestionsComponent implements OnInit {

    role: string = 'student';

    constructor(private readonly postLoggedService: PostLoggedService) {}

    ngOnInit(): void {
        this.role = this.postLoggedService.getRole();
    }
}
