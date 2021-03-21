import {SetUserSettingsSuccessPayload} from '../../../actions/payloads/user/set-user-settings.payload';
import {SetUserSettingsRequest} from '../../../../services/models/user/set-user-settings.model';

export function mapSetUserSettingsSuccessPayload(request: SetUserSettingsRequest): SetUserSettingsSuccessPayload {
  return {
    displayName: request.displayName,
    gpgKey: request.gpgKey
  };
}
