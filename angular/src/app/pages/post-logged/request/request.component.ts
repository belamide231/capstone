import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { PendingClassComponent } from "../../../../components/pending-class/pending-class.component";
import { PendingDepartmentsComponent } from "../../../../components/pending-departments/pending-departments.component";
import { PendingPostComponent } from "../../../../components/pending-post/pending-post.component";
import { PostLoggedService } from '../service/post-logged.service';
import { RequestService } from './request.service';

@Component({
    standalone: true,
    imports: [
        CommonModule,
        PendingClassComponent,
        PendingDepartmentsComponent,
        PendingPostComponent
    ],
    templateUrl: './request.component.html',
    styleUrl: './request.component.sass'
})
export class RequestComponent implements OnInit {
    id: string = '';
    email: string = '';
    role: string = '';

    listOfPendingDepartments: any[] = [];

    constructor(private readonly postLoggedService: PostLoggedService, private readonly service: RequestService) {}

    ngOnInit(): void {
        this.role = this.postLoggedService.getRole();
    }
}
