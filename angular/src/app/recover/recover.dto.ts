import { Cookie } from "../../helpers/cookie.helper";

export class RecoverDTO {

    public static VerifyEmailRecoveryDTO = class {
        constructor(public email: string) {}
    }

    public static VerifyCodeRecoveryDTO = class {
        constructor(public email: string, public code: string) {}
    }

    public static NewPassword = class {
        public email: string = "";
        public password: string = "";
        public trust: boolean = false;
        public deviceIdIdentifier: string = "";
        public deviceId: string = "";

        constructor(email: string, password: string, trust: boolean) {
            this.email = email;
            this.password = password;
            this.trust = trust;
            this.deviceIdIdentifier = Cookie.getCookie("deviceIdIdentifier");
            this.deviceId = Cookie.getCookie("deviceId");
        }
    }
}