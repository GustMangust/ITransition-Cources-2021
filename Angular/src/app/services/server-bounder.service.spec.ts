import { TestBed } from '@angular/core/testing';

import { ServerBounderService } from './server-bounder.service';

describe('ServerBounderService', () => {
  let service: ServerBounderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServerBounderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
