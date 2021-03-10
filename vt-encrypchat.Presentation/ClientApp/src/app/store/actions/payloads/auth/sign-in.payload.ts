import {SignInRequest} from '../../../../services/models/auth/sign-in.model';

export interface SignInPayload {
  request: SignInRequest;
  gpgKey: string;
}

export interface SignInSuccessPayload {
  userId: string;
  gpgKey: string;
}
