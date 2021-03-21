import {SetUserSettingsRequest} from '../../../../services/models/user/set-user-settings.model';

export interface SetUserSettingsPayload {
  request: SetUserSettingsRequest;
}

export interface SetUserSettingsSuccessPayload {
  displayName: string;
  gpgKey: string;
}
