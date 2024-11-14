import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentRequestComponent } from './department-request.component';

describe('DepartmentRequestComponent', () => {
  let component: DepartmentRequestComponent;
  let fixture: ComponentFixture<DepartmentRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DepartmentRequestComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DepartmentRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
