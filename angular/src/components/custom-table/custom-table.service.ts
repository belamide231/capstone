import { Injectable } from '@angular/core';
import { api } from '../../helpers/api.helper';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class CustomTableService {

    listOfUsersData = new BehaviorSubject<any[]>([]);
    setListOfUsersData = this.listOfUsersData.asObservable();

    async queryUsers(role: string) {

        try {

            const result = await api.post('api/users/allUsers?role=' + role);
            this.listOfUsersData.next(result.data);

       } catch (error: any) {

            console.log(error);
       }

    }

    async saveChange(email: string, role: string) {

        try {

            const result = await api.post(`api/users/saveChange?email=${email}&role=${role}`);
            if(result.status === 200) {
                this.listOfUsersData.value.find(f => {
                    if(f.Email === email) {
                        f.Roles = [role];
                    }
                });
            }

        } catch (error: any) {

            console.log(error);
        }
    }
}
