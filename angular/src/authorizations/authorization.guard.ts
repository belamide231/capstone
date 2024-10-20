import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { Cookie } from '../helpers/cookie.helper';

export const authorizationGuard: CanActivateFn = (route, state) => {
    const router = inject(Router);
    const token = Cookie.getCookie("token");
    const path = route.routeConfig?.path?.toString();


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
