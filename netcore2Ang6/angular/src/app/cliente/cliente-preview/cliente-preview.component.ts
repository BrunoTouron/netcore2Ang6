import { Component, OnInit } from '@angular/core';
import { DefaultModel } from '../../models/default';
import { DefaultService } from '../../services/default.service';
import { ActivatedRoute } from '../../../../node_modules/@angular/router';

@Component({
  selector: 'app-cliente-preview',
  templateUrl: './cliente-preview.component.html',
  styleUrls: ['./cliente-preview.component.css']
})
export class ClientePreviewComponent implements OnInit {
  model = new DefaultModel();

  constructor(protected service: DefaultService, protected activatedRoute: ActivatedRoute) {
    service.setRoute('clientes');
  }

  ngOnInit() {
    this.service.getById(this.activatedRoute.snapshot.params.id).subscribe(
      (modelAPI) => {
          this.model = modelAPI;
      });
  }

}
