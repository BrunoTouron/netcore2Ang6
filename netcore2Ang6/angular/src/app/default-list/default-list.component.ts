import { Component, OnInit, Input, TemplateRef } from '@angular/core';
import { DefaultModel } from '../models/default';
import { BsModalRef, BsModalService } from '../../../node_modules/ngx-bootstrap/modal';
import { DefaultService } from '../services/default.service';
import { Router } from '@angular/router';
@Component({
  // tslint:disable-next-line:component-selector
  selector: 'crm-default-list',
  templateUrl: './default-list.component.html',
  styleUrls: ['./default-list.component.css']
})
export class DefaultListComponent implements OnInit {
  @Input() fieldsList: string[];
  @Input() labelsList: string[];
  @Input() masksList: string[] = [];
  @Input() route: string;
  @Input() apiRoute: string;
  @Input() values: DefaultModel[] = [];
  @Input() hasFilter = false;
  @Input() title: string;
  public page: number;
  public filters: any = {};
  public bsModalRef: BsModalRef;
  public idDelete: number;
  public loading = false;
  public errorMessage: string;

  constructor(public service: DefaultService,
    public modalService: BsModalService,
    protected router: Router) { }

  public applyFilter() {
    let parameters = '';

    for (const key in this.filters) {
      if (this.filters.hasOwnProperty(key)) {
        if (parameters !== '') {
          parameters = parameters + '/';
        }

        let value = this.filters[key];

        if (value === '') {
          value = 'null';
        }

        parameters = parameters + value;
      }
    }

    this.service.getFilter('/filtro/' + parameters).subscribe(
      (valuesAPI) => {
        this.values = valuesAPI;
      });
  }

  openModalConfirmationDelete(template: TemplateRef<any>, id: number) {
    this.idDelete = id;
    this.bsModalRef = this.modalService.show(template, { class: 'modal-sm' });
    /*let values3: DefaultModel[] = [];
    //this.values	=	this.values.filter((value)	=> value.id !== id );
    console.log(values3);
    const initialState = {
      message: 'Deseja excluir o registro?',
      confirmMethod: this.confirmDelete,
      declineMethod: this.declineDelete,
      id: id,
      service: this.service,
      values: this.values,
      voted: this.onConfirmDel
    };
    this.bsModalRef = this.modalService.show(crmMessageConfirmationComponent, {initialState});*/

  }

  confirmDelete(): void {
    this.service.delete(this.idDelete).subscribe(
        () => {
          this.values = this.values.filter((value) => value.id !== this.idDelete);
        },
        (erro) => {
          this.errorMessage = erro.error;
          console.log(erro);
        });

    this.bsModalRef.hide();
  }

  declineDelete(): void {
    this.bsModalRef.hide();
  }

  newRegister() {
    this.router.navigate([this.route + '-form']);
  }

  preview(indice: number) {
    this.router.navigate([this.route + '-preview/' + indice]);
  }

  edit(indice: number) {
    this.router.navigate([this.route + '-form/' + indice]);
  }

  ngOnInit() {
    this.service.setRoute(this.apiRoute);
    this.loading = true;
    this.service.get().subscribe(
      (valuesAPI) => this.values = valuesAPI,
      (erro) => this.errorMessage = erro.error,
      () => this.loading = false);
  }

}
