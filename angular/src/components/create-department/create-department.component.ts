import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CreateDepartmentService } from './create-department.service';
import { PostLoggedService } from '../../app/pages/post-logged/service/post-logged.service';

@Component({
    selector: 'create-department',
    standalone: true,
    imports: [
        CommonModule,
        FormsModule,
    ],
    templateUrl: './create-department.component.html',
    styleUrl: './create-department.component.sass'
})
export class CreateDepartmentComponent {
    creating: boolean = false;
    departmentName: string = '';

    constructor(private readonly service: CreateDepartmentService, private readonly postLoggedServices: PostLoggedService) {}

    onCreatingSwitch() {
        this.creating = !this.creating;
    }

    onCreatingDepartment() {
        this.service.createDepartment(this.departmentName);
    }
}
