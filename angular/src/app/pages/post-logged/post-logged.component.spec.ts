import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostLoggedComponent } from './post-logged.component';

describe('PostLoggedComponent', () => {
  let component: PostLoggedComponent;
  let fixture: ComponentFixture<PostLoggedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PostLoggedComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PostLoggedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
