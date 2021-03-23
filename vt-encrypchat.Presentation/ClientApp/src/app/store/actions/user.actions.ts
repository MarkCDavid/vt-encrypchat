import { createAction, props } from '@ngrx/store';
import {GetUserSettingsPayload, GetUserSettingsSuccessPayload} from './payloads/user/get-user-settings.payload';
import {SetUserSettingsPayload, SetUserSettingsSuccessPayload} from './payloads/user/set-user-settings.payload';
import {GeneralErrorPayload} from './payloads/shared/general-error.payload';
import {GetUsersPayload, GetUsersSuccessPayload} from './payloads/user/get-users.payload';
import {LoadUserPublicKeyPayload} from "./payloads/user/load-user-public-key.payload";

export enum UserActions {
  GetUserSettings = '[User] Get User Settings',
  GetUserSettingsSuccess = '[User] Get User Settings Success',
  GetUserSettingsFail = '[User] Get User Settings Fail',
  SetUserSettings = '[User] Set User Settings',
  SetUserSettingsSuccess = '[User] Set User Settings Success',
  SetUserSettingsFail = '[User] Set User Settings Fail',
  LoadUserPublicKey = '[User] Load User Public Key',
  GetUsers = '[User] Get Users',
  GetUsersSuccess = '[User] Get Users Success',
  GetUsersFail = '[User] Get Users Fail',
}

export const getUserSettings = createAction(UserActions.GetUserSettings, props<{ payload: GetUserSettingsPayload }>());

export const getUserSettingsSuccess = createAction(UserActions.GetUserSettingsSuccess, props<{ payload: GetUserSettingsSuccessPayload }>());

export const getUserSettingsFail = createAction(UserActions.GetUserSettingsFail);

export const setUserSettings = createAction(UserActions.SetUserSettings, props<{ payload: SetUserSettingsPayload }>());

export const setUserSettingsSuccess = createAction(UserActions.SetUserSettingsSuccess, props<{ payload: SetUserSettingsSuccessPayload}>());

export const setUserSettingsFail = createAction(UserActions.SetUserSettingsFail, props<{ payload: GeneralErrorPayload }>());

export const loadUserPublicKey = createAction(UserActions.LoadUserPublicKey, props<{ payload: LoadUserPublicKeyPayload}>());

export const getUsers = createAction(UserActions.GetUsers, props<{ payload: GetUsersPayload }>());

export const getUsersSuccess = createAction(UserActions.GetUsersSuccess, props<{ payload: GetUsersSuccessPayload }>());

export const getUsersFail = createAction(UserActions.GetUsersFail);
