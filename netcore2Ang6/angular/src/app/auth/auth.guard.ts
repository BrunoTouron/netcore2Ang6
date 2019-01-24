import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { DadosLogin } from '../singleton/dados-login.singleton';

@Injectable({ providedIn: 'root'})
export class AuthGuard implements CanActivate {

    constructor(
        private dadosLogin: DadosLogin,
        private router: Router) {}

    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {

            if (!this.dadosLogin.isLogged()) {
                this.router.navigate(
                    ['']
                );
                return false;
            }
            return true;
    }
}
