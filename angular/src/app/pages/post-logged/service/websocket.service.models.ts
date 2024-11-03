export class websocketModels {

    public sendMessageModel(conversationId: string, sender: string, receivers: string[], message: string) {
        const messageModel = `
            message;
            {
                conversationId: ${conversationId},
                sender: ${sender},
                receivers: ${receivers},
                message: ${message}
            }
        `;
    }
}