import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PostLoggedService } from '../../app/pages/post-logged/service/post-logged.service';
import { DepartmentsService } from '../../app/pages/post-logged/departments/departments.service';

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
    departmentDescription: string = '';

    constructor(private readonly service: DepartmentsService, private readonly postLoggedServices: PostLoggedService) {}

    onCreatingSwitch(value: boolean) {
        this.creating = value;
    }

    onCreatingDepartment() {
        this.service.createDepartment(this.departmentName, this.departmentDescription);
        this.creating = false;
        this.departmentName = '';
        this.departmentDescription = '';
    }
}
