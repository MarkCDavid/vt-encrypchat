import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { tap } from 'rxjs/operators';
import {
  getUsers,
  getUserSettings,
  getUserSettingsFail,
  getUserSettingsSuccess, getUsersFail, getUsersSuccess,
  setUserSettings,
  setUserSettingsFail,
  setUserSettingsSuccess, toastError, toastOK
} from '../actions';
import {UserSettingsService} from '../../services/user-settings.service';
import {Store} from '@ngrx/store';
import {GetUserSettingsResponse} from '../../services/models/user/get-user-settings.model';
import {mapGetUserSettingsSuccessPayload} from './mappers/user/get-user-settings.mapper';
import {mapSetUserSettingsSuccessPayload} from './mappers/user/set-user-settings.mapper';
import {mapGeneralError} from './mappers/shared/general-error.mapper';
import {GeneralError} from '../../models/general-error';
import {UsersService} from '../../services/users.service';
import {GetUsersResponse} from '../../services/models/user/get-users.model';
import {mapGetUsersSuccessPayload} from './mappers/user/get-users.mapper';

@Injectable()
export class UserEffects {
  constructor(private actions$: Actions,
              private store: Store,
              private usersService: UsersService,
              private userSettingsService: UserSettingsService) {}

  public getUserSettings$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(getUserSettings),
        tap(({ payload }) => {
          this.userSettingsService.GetUserSettings(payload.request).subscribe(
            (response: GetUserSettingsResponse) => {
              this.store.dispatch(getUserSettingsSuccess({ payload: mapGetUserSettingsSuccessPayload(response) }));
            },
            () => {
              this.store.dispatch(getUserSettingsFail());
            }
          );
        })
      ),
    { dispatch: false }
  );

  public setUserSettings$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(setUserSettings),
        tap(({ payload }) => {
          this.userSettingsService.SetUserSettings(payload.request).subscribe(
            () => {
              this.store.dispatch(setUserSettingsSuccess({ payload: mapSetUserSettingsSuccessPayload(payload.request) }));
            },
          (generalError: GeneralError) => {
              this.store.dispatch(setUserSettingsFail( { payload: mapGeneralError(generalError) }));
            }
          );
        })
      ),
    { dispatch: false }
  );

  public setUserSettingsSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(setUserSettingsSuccess),
        tap(({ payload }) => {
          this.store.dispatch(toastOK( { message: 'Settings updated successfully' }));
        })
      ),
    { dispatch: false }
  );

  public setUserSettingsFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(setUserSettingsFail),
        tap(({ payload }) => {
          this.store.dispatch(toastError( { message: payload.generalError.error }));
        })
      ),
    { dispatch: false }
  );

  public getUsers$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(getUsers),
        tap(({ payload }) => {
          this.usersService.GetUsers(payload.request).subscribe(
            (response: GetUsersResponse) => {
              this.store.dispatch(getUsersSuccess({ payload: mapGetUsersSuccessPayload(response) }));
            },
            () => {
              this.store.dispatch(getUsersFail());
            }
          );
        })
      ),
    { dispatch: false }
  );
}
