import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LoginService } from '../services/login.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  formLogin: FormGroup;
  showMessages = false;
  errorMessage: string;

  constructor(protected service: LoginService,
    protected activatedRoute: ActivatedRoute,
    protected router: Router,
    private formBuilder: FormBuilder) { }

  public async login() {
    this.showMessages = false;

    if (this.formLogin.invalid) {
      this.showMessages = true;
    } else {
      if (await this.service.login(this.formLogin.get('usuario').value, this.formLogin.get('senha').value)) {
        this.router.navigate(['cliente']);
        location.reload();
      } else {
        this.showMessages = true;
        this.errorMessage = 'Usuário e/ou senha inválidos';
      }
    }
  }

  ngOnInit(): void {
    this.formLogin = this.formBuilder.group({
      usuario: ['', Validators.required],
      senha: ['', Validators.required]
    });
  }
}
