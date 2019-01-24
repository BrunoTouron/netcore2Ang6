import { Component, OnInit } from '@angular/core';
import { DefaultListComponent } from '../../default-list/default-list.component';

@Component({
  selector: 'app-cliente-listagem',
  templateUrl: './cliente-listagem.component.html',
  styleUrls: ['./cliente-listagem.component.css']
})
export class ClienteListagemComponent extends DefaultListComponent implements OnInit {
  filters = {nome: '', cpf: '', status: ''};

  ngOnInit() {
  }

}
