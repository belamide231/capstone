import { Component, OnInit } from '@angular/core';
import { DepartmentListComponent } from "../../../../components/department-list/department-list.component";
import { CommonModule } from '@angular/common';
import { CreateDepartmentComponent } from "../../../../components/create-department/create-department.component";
import { PostLoggedService } from '../service/post-logged.service';
import { DepartmentsService } from './departments.service';

@Component({
    standalone: true,
    imports: [CommonModule, DepartmentListComponent, CreateDepartmentComponent],
    templateUrl: './departments.component.html',
    styleUrl: './departments.component.sass'
})
export class DepartmentsComponent implements OnInit {

    role: string = '';

    listOfDepartment: any[] = [
        {
            'departmentName': 'BSHM',
            'membersId': [1,2,3,4,5],
            'creatorId': 'Belamidemills29@gmail.com'
        },
        {
            'departmentName': 'BSIT',
            'membersId': [1,2,3,4,5],
            'creatorId': 'Belamidemills29@gmail.com'
        },
        {
            'departmentName': 'DEVCOM',
            'membersId': [1,2,3,4,5],
            'creatorId': 'Belamidemills29@gmail.com'
        },
        {
            'departmentName': 'EDUC',
            'membersId': [1,2,3,4,5],
            'creatorId': 'Belamidemills29@gmail.com'
        }
    ];

    constructor(private readonly postLoggedService: PostLoggedService, private readonly service: DepartmentsService) {}

    ngOnInit(): void {
        this.service.getAllDepartments();
        this.service.setListOfDepartment.subscribe(value => this.listOfDepartment = value );
    }
}
