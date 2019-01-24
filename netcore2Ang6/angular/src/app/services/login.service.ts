import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { DefaultModel } from '../models/default';
import { ConfigurationParameters } from '../singleton/configuration.singleton';
import { DadosLogin } from '../singleton/dados-login.singleton';
import { Usuario } from '../interface/usuario.interface';

@Injectable({
    providedIn: 'root'
})
export class LoginService {
    routeUrl = '';

    constructor(protected configuration: ConfigurationParameters,
        protected dadosLogin: DadosLogin,
        protected apiConnection: HttpClient) {
        console.log('default create');
    }


    public async login(usuario: string, senha: string): Promise<boolean> {
        let validLogin = false;
        await this.apiConnection.post(this.configuration.getApiUrl() + 'login', { userId: usuario, Password: senha }).toPromise()
        .then( ret => {
            const respond = JSON.stringify(ret);

            this.dadosLogin.setUsuario(ret['nome']);
            this.dadosLogin.setToken(ret['accessToken']);

            validLogin = true;
        })
        .catch( error => console.error(error));
        return validLogin;
        /*return this.apiConnection
      .post(
        this.configuration.getApiUrl() + 'login', {userId: usuario, Password: senha})
      .pipe(tap(res => {
        console.log(res['nome']);
        console.log(res['accessToken']);
         this.dadosLogin.setUsuario(res['nome']);
         this.dadosLogin.setToken(res['accessToken']);
        })) ;*/
    }
}
