import { Component, OnInit } from '@angular/core';
import { DepartmentListComponent } from "../../../../components/department-list/department-list.component";
import { CommonModule } from '@angular/common';
import { CreateDepartmentComponent } from "../../../../components/create-department/create-department.component";
import { PostLoggedModule } from '../post-logged.module';
import { PostLoggedService } from '../service/post-logged.service';

@Component({
    standalone: true,
    imports: [CommonModule, DepartmentListComponent, CreateDepartmentComponent],
    templateUrl: './department.component.html',
    styleUrl: './department.component.sass'
})
export class DepartmentComponent implements OnInit {

    role: string = '';

    listedDepartment: any = [
        {
            'Department': 'BSHM',
            'MembersQuantity': 3012,
            'Creator': 'Belamidemills29@gmail.com'
        },
        {
            'Department': 'BSIT',
            'MembersQuantity': 3012,
            'Creator': 'Belamidemills29@gmail.com'
        },
        {
            'Department': 'DEVCOM',
            'MembersQuantity': 3012,
            'Creator': 'Belamidemills29@gmail.com'
        },
        {
            'Department': 'EDUC',
            'MembersQuantity': 3012,
            'Creator': 'Belamidemills29@gmail.com'
        }
    ];

    constructor(private readonly postLoggedService: PostLoggedService) {}

    ngOnInit(): void {
        this.role = this.postLoggedService.getRole();
    }
}
