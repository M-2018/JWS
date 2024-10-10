import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CycleSubjectComponent } from './cycle-subject.component';

describe('CycleSubjectComponent', () => {
  let component: CycleSubjectComponent;
  let fixture: ComponentFixture<CycleSubjectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CycleSubjectComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CycleSubjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
