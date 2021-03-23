import {CheckAuthenticationPayload} from "../../../actions/payloads/auth/check-authentication.payload";
import {LoadUserPublicKeyPayload} from "../../../actions/payloads/user/load-user-public-key.payload";

export function mapLoadUserPublicKeyPayload(request: CheckAuthenticationPayload): LoadUserPublicKeyPayload {
  return {
    publicKey: request.publicKey
  };
}
