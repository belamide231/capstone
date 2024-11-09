import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CustomTableService } from './custom-table.service';

@Component({
    selector: 'custom-table',
    standalone: true,
    imports: [
        FormsModule,
        CommonModule
    ],
    templateUrl: './custom-table.component.html',
    styleUrls: ['./custom-table.component.sass']
})
export class CustomTableComponent {

    usersList: any[] = [];

    constructor(private readonly service: CustomTableService) {}

    ngOnInit(): void {
        this.service.queryUsers();
    }

    @Input() users = [
        {
            'id': '20210090',
            'username': 'Timoy231@',
            'email': 'timoy@gmail.com',
            'role': 'A',
        },
        {
            'id': '20210091',
            'username': 'Helsi231@',
            'email': 'helsi@gmail.com',
            'role': 'B',
        },        
        {
            'id': '20210092',
            'username': 'Bensoy231@',
            'email': 'bensoy@gmail.com',
            'role': 'B',
        }
    ]
}
