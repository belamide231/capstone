import { Injectable } from '@angular/core';
import { api } from '../../../../helpers/api.helper';
import { Authorization } from '../../../../helpers/authorization.helper';
import { BehaviorSubject } from 'rxjs';


@Injectable({
    providedIn: 'root'
})
export class HomeService {

    listOfPostInHome = new BehaviorSubject<any[]>([]);
    setListOfPostInHome = this.listOfPostInHome.asObservable();

    async getPost() {

        try {

            const result = await api.post('api/post/getHomePosts', null, Authorization());
            this.listOfPostInHome.next(result.data);

        } catch (error: any) {

            console.log(error);
        }
    }

    async getPendingPost() {

        try {

            const result = await api.post('api/post/getHomePendingPosts', null, Authorization());
            console.log("PENDING POSTS " + result.data);

        } catch (error: any) {

            console.log(error);
        }
    }

    async onPost(description: string) {

        try {

            const result = await api.post('api/post/postInHome', {
                'Description': description,
                'In': 'Home'
            }, Authorization());
            console.log("POSTING IN HOME " + result.data);

        } catch (error: any) {

            console.log(error);
        }
    }
}
