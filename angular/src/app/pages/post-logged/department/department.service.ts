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

    queriedUsers = new BehaviorSubject<any>(null);
    setQueriedUsers = this.queriedUsers.asObservable();

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

    async GetUsersToAdd(departmentName: string, role: string) {

        try {

            const result = await api.post(`api/users/getUsersToAddInDepartment?department=${departmentName}&role=${role}`, null, Authorization());
            this.queriedUsers.next(result.data);

        } catch (error: any) {

            console.log(error);
        }                   
    }

    async AddMembers(departmentName: string, role: string, listOfUsersId: string[]) {

        try {

            const result = await api.post('api/department/addMembersInDepartment', {
                departmentName: departmentName,
                role: role,
                usersId: listOfUsersId,
            });
            console.log(result.status);

        } catch (error: any) {

            console.log(error);
        }
    }
}
