import { Component, ViewChild, AfterViewInit } from '@angular/core';
import { ElementRef } from '@angular/core';

@Component({
    selector: 'post-component',
    standalone: true,
    imports: [],
    templateUrl: './post-component.component.html',
    styleUrl: './post-component.component.sass'
})
export class PostComponentComponent implements AfterViewInit {

    @ViewChild('textarea') textarea!: ElementRef<HTMLTextAreaElement>;
    @ViewChild('label') label!: ElementRef<HTMLLabelElement>;
    initializedLabelHeight = (50 - 20);

    ngAfterViewInit(): void {

        this.textarea.nativeElement.addEventListener('input', () => {
            this.textarea.nativeElement.style.height = 'fit-content';

            console.log(this.textarea.nativeElement.scrollHeight);


            if(this.textarea.nativeElement.scrollHeight > 160) {
                this.textarea.nativeElement.style.overflowY = 'scroll';
                this.textarea.nativeElement.style.height = 160 + 'px';
                this.label.nativeElement.style.height = this.initializedLabelHeight + 160 + 'px';
            } else {
                this.textarea.nativeElement.style.overflowY = 'hidden';
                this.textarea.nativeElement.style.height = this.textarea.nativeElement.scrollHeight + 'px';
                this.label.nativeElement.style.height = this.initializedLabelHeight + this.textarea.nativeElement.scrollHeight + "px";
            }

        })
    }
    
}
