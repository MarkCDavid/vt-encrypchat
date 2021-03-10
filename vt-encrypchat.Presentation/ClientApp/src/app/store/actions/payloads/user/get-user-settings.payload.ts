import {GetUserSettingsRequest} from '../../../../services/models/user/get-user-settings.model';

export interface GetUserSettingsPayload {
  request: GetUserSettingsRequest;
}

export interface GetUserSettingsSuccessPayload {
  displayName: string;
  gpgKey: string;
}
