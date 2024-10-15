export class RegisterModels {


    static VerifyEmailModel = class {
        constructor(
            public email: string
        ) {}
    }
    
    
    static UpdateCodeModel = class  {
        constructor(
            public email: string,
            public match: boolean
        ) {}
    }


    static CreateAccountModel = class {
        constructor(
            public email: string,
            public password: string,
            public trust: boolean = false,
            public deviceId: string = "",
            public deviceIdIdentifier: string = ""
        ) {}
    }
} 