import { Injectable } from '@angular/core';
import { api } from '../../../../helpers/api.helper';
import { Authorization } from '../../../../helpers/authorization.helper';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class DepartmentService {

    departmentData = new BehaviorSubject<any>(null);
    setDepartmentData = this.departmentData.asObservable();

    async GetDepartmentData(departmentName: string) {

        try {

            const result = await api.post(`api/department/getDepartment?departmentName=${departmentName}`, null, Authorization());

            if(result.status !== 204) {
                this.departmentData.next(result.data);
            } 

        } catch (error: any) {

            console.log(error);
        }
    }
}
