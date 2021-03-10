import { createAction, props } from '@ngrx/store';
import {GetUserSettingsPayload, GetUserSettingsSuccessPayload} from './payloads/user/get-user-settings.payload';

export enum UserActions {
  GetUserSettings = '[Routing] GetUserSettings',
  GetUserSettingsSuccess = '[Routing] GetUserSettings',
  GetUserSettingsFail = '[Routing] GetUserSettings',
}

export const getUserSettings = createAction(UserActions.GetUserSettings, props<{ payload: GetUserSettingsPayload }>());

export const getUserSettingsSuccess = createAction(UserActions.GetUserSettingsSuccess, props<{ payload: GetUserSettingsSuccessPayload }>());

export const getUserSettingsFail = createAction(UserActions.GetUserSettingsFail);
