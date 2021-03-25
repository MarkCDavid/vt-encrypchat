import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { tap } from 'rxjs/operators';
import {
  getMessages, getMessagesFail, getMessagesSuccess,
  getUsers,
  getUserSettings,
  getUserSettingsFail,
  getUserSettingsSuccess, getUsersFail, getUsersSuccess, sendMessage, sendMessageFail, sendMessageSuccess,
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
import {MessageService} from "../../services/message.service";
import {GetMessagesResponse} from "../../services/models/message/get-messages.model";
import {mapGetMessagesSuccessPayload} from "./mappers/messages/get-messages.mapper";
import {Observable} from "rxjs";

@Injectable()
export class MessagesEffects {
  constructor(private actions$: Actions,
              private store: Store,
              private messagesService: MessageService) {}

  public getMessages$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(getMessages),
        tap(async ({ payload }) => {
          (await this.messagesService.GetMessages(payload.request)).subscribe(
            async (response: Promise<GetMessagesResponse>) => {
              this.store.dispatch(getMessagesSuccess({
                payload: mapGetMessagesSuccessPayload(await response)
              }));
            },
            () => {
              this.store.dispatch(getMessagesFail());
            }
          );
        })
      ),
    { dispatch: false }
  );

  public sendMessage$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(sendMessage),
        tap(async ({ payload }) => {
          (await this.messagesService.SendMessage(payload.request)).subscribe(
            () => {
              this.store.dispatch(sendMessageSuccess());
            },
            () => {
              this.store.dispatch(sendMessageFail());
            }
          );
        })
      ),
    { dispatch: false }
  );

}
