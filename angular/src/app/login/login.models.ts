export class LoginModels {

    public static VerifyCredentialModel = class {

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
} 