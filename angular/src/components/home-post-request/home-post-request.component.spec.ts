import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomePostRequestComponent } from './home-post-request.component';

describe('HomePostRequestComponent', () => {
  let component: HomePostRequestComponent;
  let fixture: ComponentFixture<HomePostRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HomePostRequestComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HomePostRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
