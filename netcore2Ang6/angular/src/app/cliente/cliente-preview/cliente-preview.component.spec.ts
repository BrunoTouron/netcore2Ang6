import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientePreviewComponent } from './cliente-preview.component';

describe('ClientePreviewComponent', () => {
  let component: ClientePreviewComponent;
  let fixture: ComponentFixture<ClientePreviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClientePreviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientePreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
