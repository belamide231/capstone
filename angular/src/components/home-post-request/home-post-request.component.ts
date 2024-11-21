import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RequestService } from '../../app/pages/post-logged/request/request.service';

@Component({
    selector: 'home-post-request',
    standalone: true,
    imports: [
        CommonModule,
        FormsModule,
    ],
    templateUrl: './home-post-request.component.html',
    styleUrl: './home-post-request.component.sass'
})
export class HomePostRequestComponent implements OnInit {

    listOfPendingPostInHome: any[] = [];

    constructor(private readonly service: RequestService) {}

    ngOnInit(): void {
        this.service.setListOfPendingPostInHome.subscribe(value => {
            console.log(this.listOfPendingPostInHome = value);
            this.listOfPendingPostInHome = value
        });
        this.service.getPendingPostInHome();
    }

    onApprovePostInHome(PendingPostInHomeId: string) {
        
        this.service.approvePendingPostInHome(PendingPostInHomeId);
    }

    onDeclinePostInHome(PendingPostInHomeId: string) {
        this.service.declinePendingPostInHome(PendingPostInHomeId);
    }
}
