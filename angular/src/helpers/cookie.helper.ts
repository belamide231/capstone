export class Cookie {

    public static getCookie(cookieName: string): string {
        
        const cookie = document.cookie.split(cookieName + "=")[1];
        
        if(cookie === undefined) 
            return "";

        return cookie.split(";")[0];
    }


    public static deleteCookie(cookieName: string, cookiePath: string): void {

        document.cookie = `${cookieName}=; max-age=0; path=${cookiePath};`;
    }
}