export class LoginDTO {

    public static VerifyCredentialDTO = class {

        public username: string = "";
        public password: string = "";
        public deviceId: string = "";
        public deviceIdIdentifier: string = "";

        constructor(username: string, password: string) {

            this.username = username;
            this.password = password;
            this.deviceIdIdentifier = document.cookie.split("deviceIdIdentifier=")[1] === undefined ? "" : document.cookie.split("deviceIdIdentifier=")[1].split(";")[0];
            this.deviceId = document.cookie.split("deviceId=")[1] === undefined ? "" : document.cookie.split("deviceId=")[1].split(";")[0];
        }
    } 
    

    public static VerifyLoginCodeDTO = class {
        
        public username: string = "";
        public code: string = "";
        public trust: boolean = false;
        public deviceIdIdentifier: string = "";
        public deviceId: string = "";

        constructor(username: string, code: string, trust: boolean) {

            this.username = username;
            this.code = code;
            this.trust = trust;
            this.deviceIdIdentifier = document.cookie.split("deviceIdIdentifier=")[1] === undefined ? "" : document.cookie.split("deviceIdIdentifier=")[1].split(";")[0];
            this.deviceId = document.cookie.split("deviceId=")[1] === undefined ? "" : document.cookie.split("deviceId=")[1].split(";")[0];
        }
    }
} 