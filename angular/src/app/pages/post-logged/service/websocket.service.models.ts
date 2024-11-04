import { TemplateBindingParseResult } from "@angular/compiler";

export class websocketModels {

    public static sendMessageModel(sender: string, receivers: string[], message: string) {
        receivers.push("123");
        receivers.push("231");
        const object = `message;{ \"sender\": \"${sender}\", \"receivers\": [${receivers.map(value => `\"${value}\"`).join(',')}], \"message\": \"${message}\" }`;
        return object;
    }
}