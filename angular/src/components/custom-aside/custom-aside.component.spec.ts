import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomAsideComponent } from './custom-aside.component';

describe('CustomAsideComponent', () => {
  let component: CustomAsideComponent;
  let fixture: ComponentFixture<CustomAsideComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomAsideComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustomAsideComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
