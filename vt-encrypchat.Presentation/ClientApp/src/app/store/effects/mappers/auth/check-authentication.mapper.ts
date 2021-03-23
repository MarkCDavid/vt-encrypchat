import {CheckAuthenticationPayload, CheckAuthenticationSuccessPayload} from '../../../actions/payloads/auth/check-authentication.payload';

export function mapCheckAuthenticationSuccessPayload(payload: CheckAuthenticationPayload): CheckAuthenticationSuccessPayload {
  return {
    userId: payload.userId,
    privateKey: payload.privateKey,
  };
}
