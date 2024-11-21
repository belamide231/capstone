import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { api } from '../../../../helpers/api.helper';
import { Authorization } from '../../../../helpers/authorization.helper';

@Injectable({
  providedIn: 'root'
})
export class PendingService {

    listOfStudentsPendingRequest = new BehaviorSubject<any[]>([]);
    setListOfStudentsPendingRequest = this.listOfStudentsPendingRequest.asObservable();

    async getStudentPendingRequest() {

        try {

            const result = await api.post('api/post/getStudentPendingPostInHome', null, Authorization());
            this.listOfStudentsPendingRequest.next(result.data);

        } catch (error: any) {

            console.log(error);
        }
    }

    async cancelStudentPendingPost(studentPendingPostId: string) {

        try {

            await api.post('api/post/cancelStudentPendingPost', {
                'studentsPendingPostId': studentPendingPostId
            }, Authorization());

            this.listOfStudentsPendingRequest.value.splice(this.listOfStudentsPendingRequest.value.findIndex(f => f.id === studentPendingPostId), 1);
            this.listOfStudentsPendingRequest.next(this.listOfStudentsPendingRequest.value);

        } catch (error: any) {

            console.log(error);
        }
    }
}
