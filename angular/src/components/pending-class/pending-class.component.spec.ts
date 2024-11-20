import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PendingClassComponent } from './pending-class.component';

describe('PendingClassComponent', () => {
  let component: PendingClassComponent;
  let fixture: ComponentFixture<PendingClassComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PendingClassComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PendingClassComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
