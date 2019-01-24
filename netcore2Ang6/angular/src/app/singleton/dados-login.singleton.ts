import { Injectable } from '@angular/core';
import { constructor } from 'events';
import { BehaviorSubject } from 'rxjs';
import { Usuario } from '../interface/usuario.interface';

@Injectable({
    providedIn: 'root'
})

export class DadosLogin {
    private userSubject = new BehaviorSubject<Usuario>(null);
    private usuario = '';
    private token = '';

    constructor() {
        this.setUsuario(sessionStorage.getItem('usuario'));
        this.setToken(sessionStorage.getItem('token'));
    }

    getUsuario(): string {
        return this.usuario;
    }

    getToken(): string {
        return this.token;
    }

    setToken(value: string) {
        this.token = value;
        sessionStorage.setItem('token', value);
    }

    setUsuario(value: string) {
        // tslint:disable-next-line:prefer-const
        let u = new Usuario();
        u.nome = value;

        this.usuario = value;
        sessionStorage.setItem('usuario', value);

        this.userSubject.next(u);
    }

    hasToken() {
        return !!this.getToken();
    }

    isLogged() {
        return this.hasToken();
    }

    getUser() {
        return this.userSubject.asObservable();
    }

    logout() {
        this.setUsuario('');
        this.setToken('');
        sessionStorage.removeItem('token');
        sessionStorage.removeItem('usuario');
    }
}
