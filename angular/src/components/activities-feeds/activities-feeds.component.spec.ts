import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActivitiesFeedsComponent } from './activities-feeds.component';

describe('ActivitiesFeedsComponent', () => {
  let component: ActivitiesFeedsComponent;
  let fixture: ComponentFixture<ActivitiesFeedsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ActivitiesFeedsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ActivitiesFeedsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
