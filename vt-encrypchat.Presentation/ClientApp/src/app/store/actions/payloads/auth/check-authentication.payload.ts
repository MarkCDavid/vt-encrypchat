export interface CheckAuthenticationPayload {
  privateKey: string;
  publicKey: string;
  userId: string;
}

export interface CheckAuthenticationSuccessPayload {
  privateKey: string;
  userId: string;
}
