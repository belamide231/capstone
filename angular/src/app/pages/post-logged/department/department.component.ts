import { Component } from '@angular/core';
import { DepartmentListComponent } from "../../../../components/department-list/department-list.component";
import { CommonModule } from '@angular/common';

@Component({
    standalone: true,
    imports: [CommonModule, DepartmentListComponent],
    templateUrl: './department.component.html',
    styleUrl: './department.component.sass'
})
export class DepartmentComponent {

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
}
