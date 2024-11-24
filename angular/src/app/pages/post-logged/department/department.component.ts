import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DepartmentService } from './department.service';
import { RequestPostComponent } from "../../../../components/request-post/request-post.component";
import { PostComponentComponent } from "../../../../components/post-component/post-component.component";
import { PostLoggedService } from '../service/post-logged.service';
import { FeedsComponentComponent } from "../../../../components/feeds-component/feeds-component.component";
import { AddMembersComponent } from "../../../../components/add-members/add-members.component";

@Component({
    standalone: true,
    imports: [
    CommonModule,
    RequestPostComponent,
    PostComponentComponent,
    FeedsComponentComponent,
    AddMembersComponent
],
    templateUrl: './department.component.html',
    styleUrl: './department.component.sass'
})
export class DepartmentComponent {
    departmentName: string = '';
    departmentData: any = null;
    departmentMembersQuantity: number = 0;
    departmentRole: string = ''
    addMode: boolean = false;
    membersToAdd: string[] = [];

    constructor(private readonly route: ActivatedRoute, private readonly service: DepartmentService, private readonly postLoggedService: PostLoggedService) {}

    ngOnInit(): void {
        this.departmentName = this.route.snapshot.paramMap.get('departmentName')!;
        this.service.GetDepartmentData(this.route.snapshot.paramMap.get('departmentName')!);
        this.service.setDepartmentData.subscribe(value => {
            if(value !== null) {
                this.departmentData = value;
                if(this.postLoggedService.getId() === value[0].creatorId) {
                    this.departmentRole = 'creator';
                }
                if(value[0].membersId.some((element: string) => this.postLoggedService.getId() === element)) {
                    this.departmentRole = 'student';
                }
                if(value[0].teachersId.some((element: string) => this.postLoggedService.getId() === element)) {
                    this.departmentData = 'teacher';
                }
            }
        });
    }

    addModeSwitch(value: boolean) {
        this.addMode = value;
    }

    blurAddMembers() {
        this.addMode = false;
    }
}
