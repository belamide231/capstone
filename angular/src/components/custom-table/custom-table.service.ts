import { Injectable } from '@angular/core';
import { api } from '../../helpers/api.helper';

@Injectable({
    providedIn: 'root'
})
export class CustomTableService {



    async queryUsers() {

        try {

            const result = await api.post('api/users/allUsers');
            console.log(result.data);

       } catch (error: any) {

            console.log(error);
       }

    }
}
