import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CustomTableService } from './custom-table.service';
import { PostLoggedService } from '../../app/pages/post-logged/service/post-logged.service';

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

    @Input() arrayOfUsers: any[] = [];
    Highlighted: number = -1;
    newRole: string = '';

    constructor(private readonly service: CustomTableService, private readonly postLoggedService: PostLoggedService) {}

    ngOnInit(): void {
        this.service.queryUsers(this.postLoggedService.getRole());
        this.service.setListOfUsersData.subscribe(value => this.arrayOfUsers = value);
    }

    onHighLight(index: number) {
        this.Highlighted = index;
        this.newRole = this.arrayOfUsers[index].Roles;
    }

    onSaveChange(email: string, role: string) {
        this.service.saveChange(email, role);
    }
}
