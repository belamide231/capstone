import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RequestService } from '../../app/pages/post-logged/request/request.service';

@Component({
    selector: 'pending-departments',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './pending-departments.component.html',
    styleUrl: './pending-departments.component.sass'
})
export class PendingDepartmentsComponent implements OnInit {
    @Input() listOfPendingDepartment: any[] = [];

    constructor(private readonly requestService: RequestService) {}

    ngOnInit(): void {
        this.requestService.getPendingDepartment();
        this.requestService.setListOfPendingDepartments.subscribe(value => this.listOfPendingDepartment = value);
    }

    onApprove(pendingDepartmentId: string) {
        this.requestService.approvePendingDepartment(pendingDepartmentId);
    }

    onDecline(pendingDepartmentId: string) {
        this.requestService.declinePendingDepartment(pendingDepartmentId);
    }
}
