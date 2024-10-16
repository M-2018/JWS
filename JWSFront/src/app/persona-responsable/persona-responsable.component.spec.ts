import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonaResponsableComponent } from './persona-responsable.component';

describe('PersonaResponsableComponent', () => {
  let component: PersonaResponsableComponent;
  let fixture: ComponentFixture<PersonaResponsableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PersonaResponsableComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PersonaResponsableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
