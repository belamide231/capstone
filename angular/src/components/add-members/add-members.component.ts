import { CommonModule } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DepartmentService } from '../../app/pages/post-logged/department/department.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'add-members',
    standalone: true,
    imports: [
        CommonModule,
        FormsModule
    ],
    templateUrl: './add-members.component.html',
    styleUrl: './add-members.component.sass'
})
export class AddMembersComponent implements OnInit {

    @Output() blurAddMembersMode = new EventEmitter<boolean>();
    membersToAdd: string = '';
    adding: boolean = false;
    QueriedUsers: any[] = [];
    ListOfMembersToBeAdded: string[] = [];

    constructor(private readonly service: DepartmentService, private readonly route: ActivatedRoute) {}

    ngOnInit(): void {  
        this.service.setQueriedUsers.subscribe(value => {
            this.adding = true;
            this.QueriedUsers = value
        });
    }

    toggleListOfMembersToBeAdded(event: any, userId: string) {

        if(event.target.checked) {
            this.ListOfMembersToBeAdded.push(userId);
        } else {
            this.ListOfMembersToBeAdded.splice(this.ListOfMembersToBeAdded.indexOf(userId), 1);
        }
    }

    toggleAddMembersButton() {

        console.log(this.ListOfMembersToBeAdded.length);

        if(this.ListOfMembersToBeAdded.length !== 0) {
            this.service.AddMembers(this.route.snapshot.params['departmentName'], this.membersToAdd, this.ListOfMembersToBeAdded);
        }
    }

    onBlur() {
        this.blurAddMembersMode.emit();
        this.membersToAdd = '';
        this.QueriedUsers = [];
        this.adding = false;
        this.ListOfMembersToBeAdded = [];
    }

    onMembersToAdd(role: string) {
        this.service.GetUsersToAdd(this.route.snapshot.params['departmentName'], role);
        this.membersToAdd = role;
    }
}
