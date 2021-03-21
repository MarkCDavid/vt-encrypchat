import { createAction, props } from '@ngrx/store';
import {GetUserSettingsPayload, GetUserSettingsSuccessPayload} from './payloads/user/get-user-settings.payload';
import {SetUserSettingsPayload, SetUserSettingsSuccessPayload} from './payloads/user/set-user-settings.payload';
import {GeneralErrorPayload} from './payloads/shared/general-error.payload';

export enum UserActions {
  GetUserSettings = '[Routing] Get User Settings',
  GetUserSettingsSuccess = '[Routing] Get User Settings Success',
  GetUserSettingsFail = '[Routing] Get User Settings Fail',
  SetUserSettings = '[Routing] Set User Settings',
  SetUserSettingsSuccess = '[Routing] Set User Settings Success',
  SetUserSettingsFail = '[Routing] Set User Settings Fail',
}

export const getUserSettings = createAction(UserActions.GetUserSettings, props<{ payload: GetUserSettingsPayload }>());

export const getUserSettingsSuccess = createAction(UserActions.GetUserSettingsSuccess, props<{ payload: GetUserSettingsSuccessPayload }>());

export const getUserSettingsFail = createAction(UserActions.GetUserSettingsFail);

export const setUserSettings = createAction(UserActions.SetUserSettings, props<{ payload: SetUserSettingsPayload }>());

export const setUserSettingsSuccess = createAction(UserActions.SetUserSettingsSuccess, props<{ payload: SetUserSettingsSuccessPayload}>());

export const setUserSettingsFail = createAction(UserActions.SetUserSettingsFail, props<{ payload: GeneralErrorPayload }>());
