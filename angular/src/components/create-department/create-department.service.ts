import { Injectable } from '@angular/core';
import { api } from '../../helpers/api.helper';
import { CreateDepartmentDTO } from './create-department.dto';
import { Authorization } from '../../helpers/authorization.helper';

@Injectable({
    providedIn: 'root'
})
export class CreateDepartmentService {

    async createDepartment(departmentName: string) {

        try {
            
            const fetch = await api.post("/api/department/create", new CreateDepartmentDTO(departmentName), Authorization());
            console.log(fetch);

        } catch (err: any) {

            console.log(err);
        }
    }
}
