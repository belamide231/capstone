import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DepartmentService } from './department.service';
import { RequestPostComponent } from "../../../../components/request-post/request-post.component";

@Component({
    standalone: true,
    imports: [
    CommonModule,
    RequestPostComponent
],
    templateUrl: './department.component.html',
    styleUrl: './department.component.sass'
})
export class DepartmentComponent {
    departmentName: string = '';
    departmentData: any = null;
    departmentMembersQuantity: number = 0;

    constructor(private readonly route: ActivatedRoute, private readonly service: DepartmentService) {}

    ngOnInit(): void {
        this.departmentName = this.route.snapshot.paramMap.get('departmentName')!;
        this.service.GetDepartmentData(this.route.snapshot.paramMap.get('departmentName')!);
        this.service.setDepartmentData.subscribe(value => {
            if(value !== null) {
                console.log(value);
                this.departmentData = value;
            }
        });
    }
}
