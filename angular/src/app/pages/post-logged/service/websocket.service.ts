import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { websocketModels } from './websocket.service.models';

@Injectable({
  providedIn: 'root'
})
export class WebsocketService {
    activeUsers = new BehaviorSubject<string[]>([]);
    setActiveUsers = this.activeUsers.asObservable();
 
    private socket: WebSocket | null = null;
    private readonly url = 'http://localhost:3000/chat';

    connect(email: string, id: string) {
        this.socket = new WebSocket(`${this.url}?email=${email}&id=${id}`);

        this.socket.onmessage = (socket) => {
            const event = socket.data.split(';')[0];
            const data = socket.data.split(';')[1];

            if(event === 'actives') {
                const updatedActiveUsers = JSON.parse(data);
                delete updatedActiveUsers[id];
                this.activeUsers.next(Object.values(updatedActiveUsers));
            }

            if(event === 'sent') {
                const sentModel = JSON.parse(data);
                console.log(sentModel);
            }
        }
    }

    send(senderId: string, chatmateId: string, message: string) {
        const sendModel = new websocketModels.sendMessageModel(senderId, [chatmateId], message);
        this.socket?.send("message;" + JSON.stringify(sendModel));
    }
}
