import { Injectable } from '@angular/core';
import { Authorization } from '../../../../helpers/authorization.helper';
import { api } from '../../../../helpers/api.helper';
import { BehaviorSubject } from 'rxjs';
import { CreateDepartmentDTO } from '../../../../components/create-department/create-department.dto';

@Injectable({
    providedIn: 'root'
})
export class DepartmentsService {

    listOfDepartment = new BehaviorSubject<any[]>([])
    setListOfDepartment = this.listOfDepartment.asObservable(); 

    public async getAllDepartments() {

        try {

            const result = await api.post('api/department/getAllDepartments', null, Authorization());
            this.listOfDepartment.next(result.data);

        } catch (error: any) {

            console.log(error);
        }
    }

    async createDepartment(departmentName: string, departmentDescription: string) {

        try {
            
            const fetch = await api.post("/api/department/create", new CreateDepartmentDTO(departmentName, departmentDescription), Authorization());
            console.log(fetch);

        } catch (err: any) {

            console.log(err);
        }
    }
}
