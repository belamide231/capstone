import { Injectable } from '@angular/core';
import { api } from '../../../../helpers/api.helper';
import { Authorization } from '../../../../helpers/authorization.helper';


@Injectable({
    providedIn: 'root'
})
export class HomeService {

    async getPost() {

        try {

            const result = await api.post('api/post/getHomePosts', null, Authorization());
            console.log("HOME POSTS " + result.data);

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
