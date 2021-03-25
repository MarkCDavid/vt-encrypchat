import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {tap} from 'rxjs/operators';
import {
  getMessages,
  getMessagesFail,
  getMessagesSuccess,
  getRecipient,
  getRecipientFail,
  getRecipientSuccess,
  go,
  sendMessage,
  sendMessageFail,
  sendMessageSuccess
} from '../actions';
import {Store} from '@ngrx/store';
import {UsersService} from '../../services/users.service';
import {GetUsersResponse} from '../../services/models/user/get-users.model';
import {MessageService} from "../../services/message.service";
import {GetMessagesResponse} from "../../services/models/message/get-messages.model";
import {mapGetMessagesSuccessPayload} from "./mappers/messages/get-messages.mapper";
import {mapGetRecipientSuccessPayload} from "./mappers/messages/get-recipient.mapper";
import {ROUTES} from "../../shared/constants/routes.const";

@Injectable()
export class MessagesEffects {
  constructor(private actions$: Actions,
              private store: Store,
              private messagesService: MessageService,
              private usersService: UsersService) {}

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

  public getRecipient$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(getRecipient),
        tap(async ({ payload }) => {
          (await this.usersService.GetUsers(payload.request)).subscribe(
            (response: GetUsersResponse) => {
              if (!response.users || response.users.length != 1) {
                this.store.dispatch(getRecipientFail());
              }
              this.store.dispatch(getRecipientSuccess({
                payload: mapGetRecipientSuccessPayload(response)
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

  public getRecipientFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(getRecipientFail),
        tap(() => {
          this.store.dispatch(go({ path: ROUTES.Home }));
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
