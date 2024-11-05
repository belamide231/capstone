export class messageModel {

    public created: string = '';
    public conversationId: string = '';
    public messageId: string = '';
    public sender: string = '';
    public status: string = '';
    public message: string = '';
}

export class audienceLatestSeenMessage {

    public userId: string = '';
    public messageId: string = '';
}

export class audienceModel {

    public userId: string = '';
    public userEmail: string = '';
}

export class conversationModel {

    public conversationId: string = '';
    public audience: audienceModel[] = [];
    public audienceLatestSeenMessage: audienceLatestSeenMessage[] = [];
    public messages: messageModel[] = []; 
}

export class websocketModels {

    static conversationsModel = class {
        
        public conversations: conversationModel[] = [];
    }

    static sendMessageModel = class {
        
        public sender: string = '';
        public receivers: string[] = [];
        public message: string = '';

        constructor(sender: string, receivers: string[], message: string) {
            this.sender = sender;
            this.receivers = receivers;
            this.message = message;
        }
    }
}