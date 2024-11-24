import { CommonModule } from '@angular/common';
import { Component, ViewChild, AfterViewInit, Input, Output, EventEmitter, ChangeDetectorRef } from '@angular/core';
import { ElementRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PostLoggedService } from '../../app/pages/post-logged/service/post-logged.service';

@Component({
    selector: 'post-component',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './post-component.component.html',
    styleUrl: './post-component.component.sass'
})
export class PostComponentComponent implements AfterViewInit {

    email: string = '';

    @Input() description: string = '';
    @Output() descriptionEmitter = new EventEmitter<string>(); 
    @Output() postEmitter = new EventEmitter<string>();


    @ViewChild('textarea') textarea!: ElementRef<HTMLTextAreaElement>;
    @ViewChild('label') label!: ElementRef<HTMLLabelElement>;
    initializedLabelHeight = (50 - 20);

    constructor(private cdRef: ChangeDetectorRef, private readonly postLoggedService: PostLoggedService) {}

    onEmittingDescription(): void {
        this.descriptionEmitter.emit(this.description);
    }

    onEmittingPost(): void {
        this.postEmitter.emit();
    }

    ngAfterViewInit(): void {

        this.textarea.nativeElement.addEventListener('input', () => this.onUpdateTextareaHeight());
        this.email = this.postLoggedService.getEmail()[0].toUpperCase();
        this.cdRef.detectChanges();
    }

    onUpdateTextareaHeight(): void {
        this.textarea.nativeElement.style.height = 'fit-content';

        if(this.textarea.nativeElement.scrollHeight > 160) {
            this.textarea.nativeElement.style.overflowY = 'scroll';
            this.textarea.nativeElement.style.height = 160 + 'px';
            this.label.nativeElement.style.height = this.initializedLabelHeight + 160 + 'px';
        } else {
            this.textarea.nativeElement.style.overflowY = 'hidden';
            this.textarea.nativeElement.style.height = this.textarea.nativeElement.scrollHeight + 'px';
            this.label.nativeElement.style.height = this.initializedLabelHeight + this.textarea.nativeElement.scrollHeight + "px";
        }
    }

    onAnnouncement(): void {
        console.log(this.description.includes('#Announcement'));

        if(!this.description.includes('#Announcement')) {
            this.description = '#Announcement ' + this.description;
        }
        if(this.description.includes('#Question ')) {
            this.description = this.description.replace('#Question ', '');
        }

        this.onUpdateTextareaHeight();
        this.onEmittingDescription();
        this.textarea.nativeElement.focus();
    }

    onAsk(): void {
        if(!this.description.includes('#Question')) {
            this.description = '#Question ' + this.description;
        }
        if(this.description.includes('#Announcement ')) {
            this.description = this.description.replace('#Announcement ', '');
        }
        
        this.onUpdateTextareaHeight();
        this.onEmittingDescription();
        this.textarea.nativeElement.focus();
    }
}
