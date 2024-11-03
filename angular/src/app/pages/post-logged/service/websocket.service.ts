import { Injectable } from '@angular/core';
import { Cookie } from '../../../../helpers/cookie.helper';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WebsocketService {
    activeUsers = new BehaviorSubject<string[]>([]);
    activeObjects: any[] = [];
    setActiveUsers = this.activeUsers.asObservable();

    private socket: WebSocket | null = null;
    private readonly url = 'http://localhost:3000/chat';

    connect() {
        this.socket = new WebSocket(`${this.url}?token=${Cookie.getCookie('token')}`);

        this.socket.onmessage = (socket) => {
            if(socket.data.split(";")[0] === "actives") {
                this.activeUsers.next(Object.values(JSON.parse(socket.data.split(';')[1])));
                console.log(Object.keys(JSON.parse(socket.data.split(';')[1])));
                this.activeObjects = JSON.parse(socket.data.split(';')[1]);
            }
        }
    }

    send(message: string) {
        this.socket?.send(message);
        const given = 'belamidemills29@gmail.com';
        const receiversIds = Object.entries(this.activeObjects).map((object) => object[1] === given ? object[0] : null);
        console.log(receiversIds);
    }
}
