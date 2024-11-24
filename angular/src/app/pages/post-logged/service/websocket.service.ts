import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { websocketModels } from './websocket.service.models';

@Injectable({
  providedIn: 'root'
})
export class WebsocketService {

    activeUsers = new BehaviorSubject<string[]>([]);
    setActiveUsers = this.activeUsers.asObservable();

    arrayOfConversations = new BehaviorSubject<any[]>([]);
    setArrayOfConversations = this.arrayOfConversations.asObservable();

    messageNotification = new BehaviorSubject<string[]>([]);
    setMessageNotification = this.messageNotification.asObservable();
    
    private socket: WebSocket | null = null;
    private readonly url = 'ws://localhost:81/chat';

    connect(email: string, id: string) {
        this.socket = new WebSocket(`${this.url}?email=${email}&id=${id}`);

        this.socket.onmessage = (socket) => {
            const event = socket.data.split(';')[0];
            const data = socket.data.split(';')[1];

            if(event === 'initialization') {
                const initializedConversations = JSON.parse(data);
                this.arrayOfConversations.next(initializedConversations);
            }

            if(event === 'actives') {
                const updatedActiveUsers = JSON.parse(data);
                delete updatedActiveUsers[id];
                this.activeUsers.next(Object.values(updatedActiveUsers));
            }

            if(event === 'sent') {
                const sentData = JSON.parse(data);       
                console.log(sentData);         
                const index = this.arrayOfConversations.value.findIndex((object: any) => object.ConversationId === sentData.ConversationId);
                this.arrayOfConversations.value[index].Messages.unshift(sentData.Messages[0]);
                this.arrayOfConversations.next(this.arrayOfConversations.value);
            }

            if(event === 'receive') {
                const receiveData = JSON.parse(data);
                console.log(receiveData);
                const index = this.arrayOfConversations.value.findIndex((object: any) => object.Audience.sort().join() === receiveData.Audience.sort().join());
                if(index === -1) {
                    this.arrayOfConversations.value.unshift(receiveData);
                } else {
                    console.log(this.arrayOfConversations.value);
                    this.arrayOfConversations.value[index].Messages.unshift(receiveData.Messages[0]);
                    console.log(this.arrayOfConversations.value);
                }
                this.arrayOfConversations.next(this.arrayOfConversations.value);
                this.messageNotification.next(receiveData.Audience);
            }
        }
    }

    send(senderId: string, chatmateId: string, message: string) {
        const sendModel = new websocketModels.sendMessageModel(senderId, [chatmateId], message);
        this.socket?.send("message;" + JSON.stringify(sendModel));
    }
}
