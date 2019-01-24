import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MessageCarregandoComponent } from './message-carregando.component';

describe('MessageCarregandoComponent', () => {
  let component: MessageCarregandoComponent;
  let fixture: ComponentFixture<MessageCarregandoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MessageCarregandoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MessageCarregandoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
