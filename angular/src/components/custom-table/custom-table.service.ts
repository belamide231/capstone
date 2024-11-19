import { Injectable } from '@angular/core';
import { api } from '../../helpers/api.helper';
import { BehaviorSubject } from 'rxjs';
import { Authorization } from '../../helpers/authorization.helper';

@Injectable({
    providedIn: 'root'
})
export class CustomTableService {

    listOfUsersData = new BehaviorSubject<any[]>([]);
    setListOfUsersData = this.listOfUsersData.asObservable();

    async queryUsers(role: string) {

        try {

            const result = await api.post('api/users/allUsers', null, Authorization());
            this.listOfUsersData.next(result.data);

       } catch (error: any) {

            console.log(error);
       }

    }

    async saveChange(email: string, role: string) {

        try {

            const result = await api.post('api/users/saveChange', {
                'email': email,
                'role': role
            }, Authorization());

            if(result.status === 200) {

                var index = this.listOfUsersData.value.findIndex((obj: any) => obj.email === email);
                var updated = this.listOfUsersData.value;
                updated[index].roles = [role];
                this.listOfUsersData.next(updated);
            }

        } catch (error: any) {

            console.log(error);
        }
    }
}
