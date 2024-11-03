import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { Cookie } from '../helpers/cookie.helper';
import { api } from '../helpers/api.helper';
import { guardsDTO } from './guards.dto';
import { PostLoggedService } from '../app/pages/post-logged/service/post-logged.service';

export const AuthorizationGuard: CanActivateFn = async (route, state) => {
    const router = inject(Router);
    const postLoggedService = inject(PostLoggedService);
    const token = Cookie.getCookie("token");
    const path = route.routeConfig?.path?.toString();
    const endpoint = 'api/policy/user';
    let isAuthorized = true;


    if (token !== '') {

        try {

            const result = await api.post(endpoint, null, guardsDTO.authorization(token));

            if (result.status !== 200) {

                isAuthorized = false;

            } else {

                console.log(result.data);
                postLoggedService.setInfo(result.data.role, result.data.email, result.data.id);
            }

        } catch (error) {

            isAuthorized = false;
        }

    } else {

        isAuthorized = false;
    }

    if (!isAuthorized && (path !== 'login' && path !== 'register' && path !== 'recover')) {

        router.navigate(['/login']);
        return false;
    }

    if (isAuthorized && (path === 'login' || path === 'register' || path === 'recover')) {

        router.navigate(['/']);
        return false;
    }

    
    return true;
};
