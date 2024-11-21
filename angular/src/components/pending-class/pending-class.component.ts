import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RequestService } from '../../app/pages/post-logged/request/request.service';
import { PostLoggedService } from '../../app/pages/post-logged/service/post-logged.service';

@Component({
    selector: 'pending-class',
    standalone: true,
    imports: [
        CommonModule,
        FormsModule
    ],
    templateUrl: './pending-class.component.html',
    styleUrl: './pending-class.component.sass'
})
export class PendingClassComponent implements OnInit {
    deansPendingDepartment: any[] = [];

    constructor(private readonly requestService: RequestService, private readonly postLoggedService: PostLoggedService) {}

    ngOnInit(): void {
        this.requestService.setListOfDeansPendingDepartment.subscribe(value => this.deansPendingDepartment = value);
        this.requestService.getDeansPendingDepartment();
    }

    onCancelRequest(pendingDepartmentId: string) {
        this.requestService.declinePendingDepartment(pendingDepartmentId);
    } 
}
