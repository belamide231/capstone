import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'department-list',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './department-list.component.html',
    styleUrl: './department-list.component.sass'
})
export class DepartmentListComponent {
    @Input() listedDepartment: any[] = [];
    
    constructor(private readonly router: Router) {}

    onRedirections(departmentName: string) {
        this.router.navigate([`/department/${departmentName}`]);
    }
}
