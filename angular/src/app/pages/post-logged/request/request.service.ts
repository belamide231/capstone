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

    listOfDeansPendingDepartment = new BehaviorSubject<any[]>([]);
    setListOfDeansPendingDepartment = this.listOfDeansPendingDepartment.asObservable();

    async getPendingDepartment() {

        try {

            const result = await api.post("api/department/getPendingDepartments", null, Authorization());
            this.listOfPendingDepartments.next(result.data);

        } catch (error: any) {

            console.log(error);
        }
    }

    listOfPendingPostInHome = new BehaviorSubject<any[]>([]);
    setListOfPendingPostInHome = this.listOfPendingPostInHome.asObservable();

    async getPendingPostInHome() {

        try {

            const result = await api.post('api/post/getAllRequestPostInHome', null, Authorization());
            this.listOfPendingPostInHome.next(result.data);

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

            this.listOfDeansPendingDepartment.value.splice(this.listOfDeansPendingDepartment.value.findIndex(f => f.id === pendingDepartmentId), 1);
            this.listOfDeansPendingDepartment.next(this.listOfDeansPendingDepartment.value);

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

    async getDeansPendingDepartment() {

        try {
            
            const result = await api.post('api/department/getDeansPendingDepartment', null, Authorization());
            console.log(result.data);
            this.listOfDeansPendingDepartment.next(result.data);

        } catch (error: any) {
            console.log(error);;
        }
    }

    async declinePendingPostInHome(studentsPendingPostId: string) {

        try {

            await api.post("api/post/cancelStudentPendingPost", {
                'studentsPendingPostId': studentsPendingPostId
            }, Authorization());

            this.listOfPendingPostInHome.value.splice(this.listOfPendingPostInHome.value.findIndex(obj => obj.id === studentsPendingPostId), 1);
            this.listOfPendingPostInHome.next(this.listOfPendingPostInHome.value);

        } catch (error: any) {

            console.log(error);
        }
    }

    async approvePendingPostInHome(studentsPendingPostId: string) {

        try {

            const result = await api.post('api/post/approvePendingPostInHome', {
                'studentsPendingPostId': studentsPendingPostId
            }, Authorization());

            this.listOfPendingPostInHome.value.splice(this.listOfPendingPostInHome.value.findIndex(obj => obj.id === studentsPendingPostId), 1);
            this.listOfPendingPostInHome.next(this.listOfPendingPostInHome.value);            
            
            console.log(result.status);

        } catch (error: any) {

            console.log(error);
        }
    }
}
