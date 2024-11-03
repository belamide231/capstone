import { TestBed } from '@angular/core/testing';

import { PostLoggedService } from './post-logged.service';

describe('PostLoggedService', () => {
  let service: PostLoggedService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PostLoggedService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
