import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { tap } from 'rxjs/operators';
import {getUserSettings, getUserSettingsFail, getUserSettingsSuccess} from '../actions';
import {UserSettingsService} from '../../services/user-settings.service';
import {Store} from '@ngrx/store';
import {GetUserSettingsResponse} from '../../services/models/user/get-user-settings.model';
import {mapGetUserSettingsSuccessPayload} from './mappers/user/get-user-settings.mapper';

@Injectable()
export class UserEffects {
  constructor(private actions$: Actions,
              private store: Store,
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
}
