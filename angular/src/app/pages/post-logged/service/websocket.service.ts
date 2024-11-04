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
            if(socket.data.split(";")[0] === "actives") {
                const updatedActiveUsers = JSON.parse(socket.data.split(';')[1]);
                delete updatedActiveUsers[id];
                this.activeUsers.next(Object.values(updatedActiveUsers));
            }
        }
    }

    send(senderId: string, chatmateId: string, message: string) {
        this.socket?.send(websocketModels.sendMessageModel(senderId, [chatmateId], message));
    }
}
