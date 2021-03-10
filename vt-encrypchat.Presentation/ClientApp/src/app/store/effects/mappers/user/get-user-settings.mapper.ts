import {GetUserSettingsSuccessPayload} from '../../../actions/payloads/user/get-user-settings.payload';
import {GetUserSettingsResponse} from '../../../../services/models/user/get-user-settings.model';

export function mapGetUserSettingsSuccessPayload(response: GetUserSettingsResponse): GetUserSettingsSuccessPayload {
  return {
    displayName: response.displayName,
    gpgKey: response.gpgKey
  };
}
