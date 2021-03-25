import { createAction, props } from '@ngrx/store';
import {GetUserSettingsPayload, GetUserSettingsSuccessPayload} from './payloads/user/get-user-settings.payload';
import {SetUserSettingsPayload, SetUserSettingsSuccessPayload} from './payloads/user/set-user-settings.payload';
import {GeneralErrorPayload} from './payloads/shared/general-error.payload';
import {GetUsersPayload, GetUsersSuccessPayload} from './payloads/user/get-users.payload';
import {LoadUserPublicKeyPayload} from "./payloads/user/load-user-public-key.payload";
import {GetMessagesPayload, GetMessagesSuccessPayload} from "./payloads/messages/get-messages.payload";
import {SendMessageRequest} from "../../services/models/message/send-message.model";
import {SendMessagePayload} from "./payloads/messages/send-message.payload";

export enum MessagesActions {
  GetMessages = '[Messages] Get Messages',
  GetMessagesSuccess = '[Messages] Get Messages Success',
  GetMessagesFail = '[Messages] Get Messages Fail',
  SendMessage = '[Messages] Send Message',
  SendMessageSuccess = '[Messages] Send Message Success',
  SendMessageFail = '[Messages] Send Message Fail',
}

export const getMessages = createAction(MessagesActions.GetMessages, props<{ payload: GetMessagesPayload }>());

export const getMessagesSuccess = createAction(MessagesActions.GetMessagesSuccess, props<{ payload: GetMessagesSuccessPayload }>());

export const getMessagesFail = createAction(MessagesActions.GetMessagesFail);

export const sendMessage = createAction(MessagesActions.SendMessage, props<{ payload: SendMessagePayload }>());

export const sendMessageSuccess = createAction(MessagesActions.SendMessageSuccess);

export const sendMessageFail = createAction(MessagesActions.SendMessageFail);
