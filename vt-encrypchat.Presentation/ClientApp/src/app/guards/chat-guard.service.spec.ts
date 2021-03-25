import { TestBed } from '@angular/core/testing';

import { ChatGuard } from './chat-guard.service';

describe('ChatGuardService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ChatGuard = TestBed.get(ChatGuard);
    expect(service).toBeTruthy();
  });
});
