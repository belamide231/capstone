import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { api } from '../../../../helpers/api.helper';
import { Authorization } from '../../../../helpers/authorization.helper';

@Injectable({
  providedIn: 'root'
})
export class RequestService {

    listOfPendingDepartments = new BehaviorSubject<any[]>([]);
    setListOfPendingDepartments = this.listOfPendingDepartments.asObservable();

    async getPendingDepartment() {

        try {

            const result = await api.post("api/department/getPendingDepartments", null, Authorization());
            this.listOfPendingDepartments.next(result.data);

        } catch (error: any) {

            console.log(error);
        }
    }

    async declinePendingDepartment(pendingDepartmentId: string) {

        try {

            await api.post('api/department/deletePendingDepartment', {
                'pendingDepartmentId': pendingDepartmentId
            }, Authorization());

            this.listOfPendingDepartments.value.splice(this.listOfPendingDepartments.value.findIndex(f => f.id === pendingDepartmentId), 1);
            this.listOfPendingDepartments.next(this.listOfPendingDepartments.value);

        } catch (error: any) {

            console.log(error);
        }
    }

    async approvePendingDepartment(pendingDepartmentId: string) {

        try {

            const result = await api.post('api/department/approvePendingDepartment', {
                'pendingDepartmentId': pendingDepartmentId
            }, Authorization());

            this.listOfPendingDepartments.value.splice(this.listOfPendingDepartments.value.findIndex(f => f.id === pendingDepartmentId), 1);
            this.listOfPendingDepartments.next(this.listOfPendingDepartments.value);

        } catch (error: any) {

            console.log(error);
        }
    }
}
