import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { DefaultModel } from '../../models/default';
import { FormGroup, FormBuilder, Validators } from '../../../../node_modules/@angular/forms';
import { DefaultService } from '../../services/default.service';
import { ActivatedRoute, Router } from '../../../../node_modules/@angular/router';

@Component({
  selector: 'app-cliente-form',
  templateUrl: './cliente-form.component.html',
  styleUrls: ['./cliente-form.component.css']
})
export class ClienteFormComponent implements OnInit {
  model = new DefaultModel();
  formCadastro: FormGroup;
  showMessages = false;
  errorMessage: string;
  cpf: string;
  midias: DefaultModel[] = [];
  @ViewChild('nomeInput') nomeInput: ElementRef<HTMLInputElement>;

  constructor(protected service: DefaultService,
    protected activatedRoute: ActivatedRoute,
    protected router: Router,
    private formBuilder: FormBuilder) {
    service.setRoute('clientes');
  }

  public async save() {
    this.model = { ...this.model, ...this.formCadastro.value };

    this.errorMessage = '';

    if (await this.service.getExist({ cpf: this.model['cpf'] }) && (this.model.id === 0)) {
      this.errorMessage = 'Já existe um registo com este cpf';
    } else {
      if ((this.model['telCel'] === '') && (this.model['telRes'] === '')) {
        this.errorMessage = 'Preencher algum telefone';
      } else {
        if (this.formCadastro.invalid) {
          this.showMessages = true;
        } else {
          this.service.save(this.model).subscribe(() => {
            this.router.navigate(['clientes']);
          },
            (erro) => { this.errorMessage = erro.error; });
        }
      }
    }
  }

  loatAuxiliarLists() {
    this.service.getByRoute('midias').subscribe((modelAPI) => { this.midias = modelAPI; });
  }

  ngOnInit() {
    this.loatAuxiliarLists();

    this.formCadastro = this.formBuilder.group({
      nome: ['', Validators.required],
      sexo: ['', Validators.required],
      endereco: ['', Validators.required],
      cep: ['', Validators.required],
      cpf: ['', Validators.required],
      dataNascimento: ['', Validators.required],
      profissao: [''],
      telCel: [''],
      telRes: [''],
      estadoCivil: [''],
      email: [''],
      instagram: [''],
      observacao: [''],
      midiaId: ['', Validators.required],
      bloqueado: false,
      dataUltimoServico: ['']
    });

    if (this.activatedRoute.snapshot.params.id) {
      this.service.getById(this.activatedRoute.snapshot.params.id).subscribe((modelAPI) => {
        this.model = modelAPI;
        this.formCadastro.patchValue(modelAPI);
        this.formCadastro.controls['dataNascimento'].setValue(new Date(modelAPI['dataNascimento']).toISOString().substring(0, 10));
      },
        (erro) => { this.errorMessage = erro.error; });
    }
    this.nomeInput.nativeElement.focus();
  }

}
