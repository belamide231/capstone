import { CommonModule } from '@angular/common';
import { OnInit, Component, ViewChild, ElementRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { WebsocketService } from '../../app/pages/post-logged/service/websocket.service';
import { PostLoggedService } from '../../app/pages/post-logged/service/post-logged.service';
import { filter, findIndex } from 'rxjs';

@Component({
    selector: 'messenger',
    standalone: true,
    imports: [
        CommonModule,
        FormsModule
    ],
    templateUrl: './messenger.component.html',
    styleUrls: ['./messenger.component.sass']
})
export class MessengerComponent implements OnInit {
    id: string = '';
    email: string = '';

    chatmate: string = '';
    chatmateId: string = '';
    actives: any = [];
    message: string = '';
    arrayOfConversations: any = [];
    arrayOfMessages: any = [];

    bodyInitialization: number = 371;
    footerInitialization: number = 57;
    textareaInitiation: number = 17;
    textareaWidthInitation: number = 238;

    @ViewChild('body') body!: ElementRef<HTMLDivElement>;
    @ViewChild('footer') footer!: ElementRef<HTMLDivElement>;
    @ViewChild('textarea') textarea!: ElementRef<HTMLTextAreaElement>;

    @ViewChild('messageView') messageView!: ElementRef<HTMLDivElement>;
    @ViewChild('messageContainer') messageContainer!: ElementRef<HTMLDivElement>;

    constructor(private readonly websocket: WebsocketService, private readonly service: PostLoggedService) {}

    ngOnInit() {
        this.email = this.service.getEmail();
        this.id = this.service.getId();
        if(this.chatmate !== '') {
            const textarea = this.textarea.nativeElement;
            textarea.addEventListener('input', () => this.logWrapCount(textarea));
        }
        this.websocket.setActiveUsers.subscribe((value: any) => this.actives = value);
        this.websocket.setArrayOfConversations.subscribe((value: any) => this.arrayOfConversations = value);
        this.websocket.setMessageNotification.subscribe((value: string[]) => {

            this.arrayOfConversations.find((object: any) => {
                if(object.Audience.sort().join() === value.sort().join()) {
                    object.Audience.find((receiverId: string) => {
                        if(receiverId !== this.id) {
                            this.actives.find((objectActives: any) => {
                                if(receiverId === objectActives.Id) {
                                    this.onSetChatmate(objectActives.Email, objectActives.Id);
                                    return;
                                }
                            });
                        }
                    });
                }
            });
        });
    }

    onCloseChatmate() {
        this.chatmate = '';
        this.chatmateId = '';
        this.message = '';
    }

    onSetChatmate(chatmate: string, id: string) {

        this.chatmateId = id;
        this.chatmate = chatmate;

        setTimeout(() => {
            this.messageView.nativeElement.scrollTop = this.messageView.nativeElement.scrollHeight;
        }, 10);

        setTimeout(() => {
            const textarea = this.textarea.nativeElement;
            textarea.addEventListener('input', () => this.logWrapCount(textarea));
        }, 100);

        const arr = [this.id]
        arr.push(id);

        this.arrayOfConversations.find((object: any, index: number) => {
            if(arr.length === object.Audience.length && arr.sort().join() === object.Audience.sort().join()) {
                this.arrayOfMessages = object.Messages;
                return;
            }
        });
    }

    onKeyDownEvent(event: any) {
        if(event.key === 'Enter') {
            this.body.nativeElement.style.height = this.bodyInitialization + 'px';
            this.footer.nativeElement.style.height = this.footerInitialization + 'px';
            this.textarea.nativeElement.style.height = this.textareaInitiation + 'px';
            this.textarea.nativeElement.style.width = this.textareaWidthInitation + 'px';
            event.key === 'Enter' ? this.onMessage() : null;
            event.preventDefault();
        }
    }

    onMessageDefault() {
        this.textarea.nativeElement.style.height = 17 + 'px';
        this.textarea.nativeElement.style.overflow = 'hidden';
        this.textarea.nativeElement.style.width = this.textareaWidthInitation + 'px';
        this.footer.nativeElement.style.height = this.footerInitialization + 'px';
        this.body.nativeElement.style.height = this.bodyInitialization + 'px';
    }

    onMessage() {
        this.websocket.send(this.id, this.chatmateId, this.message);
        this.message = '';
        this.onMessageDefault();
        setTimeout(() => {
            this.messageView.nativeElement.scrollTop = this.messageView.nativeElement.scrollHeight;
        }, 10);
    }

    logWrapCount(textarea: HTMLTextAreaElement) {
        const wrapLimit = 6;
        this.textarea.nativeElement.style.height = 'fit-content';
        const increase = textarea.scrollHeight - 17;
        if(textarea.scrollHeight > 17 * (wrapLimit+1)) {
            textarea.style.overflowY = 'scroll';
            textarea.style.padding = ' 0 5px 0 0';
            this.textarea.nativeElement.style.width = (this.textareaWidthInitation - 5) + 'px';
            this.textarea.nativeElement.style.height = this.textarea.nativeElement.style.height = 17 * (wrapLimit + 1) + 'px';
            this.body.nativeElement.style.height = this.bodyInitialization - 17 * wrapLimit + 'px';
            this.footer.nativeElement.style.height = this.footerInitialization + 17 * wrapLimit + 'px';
        } else {
            textarea.style.overflowY = 'hidden';
            textarea.style.padding = '0';
            this.textarea.nativeElement.style.width = this.textareaWidthInitation + 'px';
            this.textarea.nativeElement.style.height = textarea.scrollHeight + "px";
            this.body.nativeElement.style.height = this.bodyInitialization - increase + 'px';
            this.footer.nativeElement.style.height = this.footerInitialization + increase + "px";
        }
    }
}
