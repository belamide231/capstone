import { Component } from '@angular/core';
import { CustomHeaderComponent } from "../../../components/custom-header/custom-header.component";
import { CustomAsideComponent } from "../../../components/custom-aside/custom-aside.component";
import { CustomTableComponent } from "../../../components/custom-table/custom-table.component";

@Component({
    selector: 'app-users',
    standalone: true,
    imports: [CustomHeaderComponent, CustomAsideComponent, CustomTableComponent],
    templateUrl: './users.component.html',
    styleUrl: './users.component.sass'
})
export class UsersComponent {

}
