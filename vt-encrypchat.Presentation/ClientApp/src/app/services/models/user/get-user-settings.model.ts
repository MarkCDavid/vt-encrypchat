export interface GetUserSettingsRequest {
  userId: string;
}

export interface GetUserSettingsResponse {
  displayName: string;
  gpgKey: string;
}
