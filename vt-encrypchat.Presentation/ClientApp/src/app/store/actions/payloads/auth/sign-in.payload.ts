import {SignInRequest} from '../../../../services/models/auth/sign-in.model';

export interface SignInPayload {
  request: SignInRequest;
  privateKey: string;
}

export interface SignInSuccessPayload {
  userId: string;
  privateKey: string;
}
