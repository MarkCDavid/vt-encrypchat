import { TestBed } from '@angular/core/testing';

import { UserSettingsGuard } from './user-settings-guard.service';

describe('UserSettingsGuardService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UserSettingsGuard = TestBed.get(UserSettingsGuard);
    expect(service).toBeTruthy();
  });
});
