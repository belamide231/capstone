import { Cookie } from "./cookie.helper";

export const Authorization = () => {
    return {
        'headers': {
            'Authorization': `Bearer ${Cookie.getCookie('token')}`,
            'Content-Type': 'application/json',
        }
    };
} 