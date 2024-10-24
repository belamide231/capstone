import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { Cookie } from '../helpers/cookie.helper';
import { api } from '../helpers/api.helper';

export const AuthorizationGuard: CanActivateFn = async (route, state) => {
    const router = inject(Router);
    const token = Cookie.getCookie("token");
    const path = route.routeConfig?.path?.toString();

    const endpoint = 'api/policy/user';
    const result = await api.post(endpoint, null, { headers: { 'Authorization': `Bearer ${Cookie.getCookie('token')}` }});
    console.log(result.status);


    if(token) {

        if(path === "login" || path === "register" || path === "recover") {

            router.navigate(["/"]);
            return false;
        }

        return true;
    }


    if(path !== "login" && path !== "register" && path !== "recover") {
    
        router.navigate(["/login"]);
        return false;
    }

    
    return true;
}
