import { Component, OnInit } from '@angular/core';
import { PostLoggedService } from './service/post-logged.service';
import { WebsocketService } from './service/websocket.service';

@Component({
    templateUrl: './post-logged.component.html',
    styleUrls: ['./post-logged.component.sass']
})
export class PostLoggedComponent implements OnInit {
    role: string = '';
    constructor(private readonly service: PostLoggedService, private readonly socket: WebsocketService) {}

    ngOnInit(): void {
        this.role = this.service.getRole();
        this.socket.connect();
    }
}