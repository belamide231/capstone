import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class PostLoggedService {
    private role: string = '';
    private email: string = '';
    private id: string = '';

    setInfo(role: string, email: string, id: string) {
        this.role = role;
        this.email = email;
        this.id = id;
    }

    getRole = () => this.role;
    getEmail = () => this.email;
    getId = () => this.id;
}
