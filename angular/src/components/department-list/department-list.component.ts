import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
    selector: 'department-list',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './department-list.component.html',
    styleUrl: './department-list.component.sass'
})
export class DepartmentListComponent {
    @Input() listedDepartment: any[] = [];
}
