import {SignInPayload, SignInSuccessPayload} from '../../../actions/payloads/auth/sign-in.payload';
import {SignInResponse} from '../../../../services/models/auth/sign-in.model';

export function mapSignInSuccessPayload(payload: SignInPayload, response: SignInResponse): SignInSuccessPayload {
  return {
    userId: response.id,
    gpgKey: payload.gpgKey
  };
}
