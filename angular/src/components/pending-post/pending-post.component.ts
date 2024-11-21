import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PostLoggedService } from '../../app/pages/post-logged/service/post-logged.service';
import { PendingService } from '../../app/pages/post-logged/pending/pending.service';

@Component({
    selector: 'pending-post',
    standalone: true,
    imports: [
        CommonModule,
        FormsModule,
    ],
    templateUrl: './pending-post.component.html',
    styleUrl: './pending-post.component.sass'
})
export class PendingPostComponent implements OnInit {
    
    studentsPendingPost: any[] = [];

    constructor(private readonly postloggedService: PostLoggedService, private readonly service: PendingService) {}

    ngOnInit(): void {
        this.service.setListOfStudentsPendingRequest.subscribe(value => {
            this.studentsPendingPost = value;
            console.log(value);
        });
        this.service.getStudentPendingRequest();
    }

    onCancelPendingPostInHome(PendingPostInHomeId: string) {
        this.service.cancelStudentPendingPost(PendingPostInHomeId);
    }
}
