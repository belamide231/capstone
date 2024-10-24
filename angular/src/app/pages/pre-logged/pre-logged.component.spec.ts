import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PreLoggedComponent } from './pre-logged.component';

describe('PreLoggedComponent', () => {
  let component: PreLoggedComponent;
  let fixture: ComponentFixture<PreLoggedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PreLoggedComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PreLoggedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
