import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PendingDepartmentsComponent } from './pending-departments.component';

describe('PendingDepartmentsComponent', () => {
  let component: PendingDepartmentsComponent;
  let fixture: ComponentFixture<PendingDepartmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PendingDepartmentsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PendingDepartmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
