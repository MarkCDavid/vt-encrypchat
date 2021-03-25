import { createAction, props } from '@ngrx/store';
import {GetMessagesPayload, GetMessagesSuccessPayload} from "./payloads/messages/get-messages.payload";
import {SendMessagePayload} from "./payloads/messages/send-message.payload";
import {GetRecipientPayload, GetRecipientSuccessPayload} from "./payloads/messages/get-recipient.payload";

export enum MessagesActions {
  GetRecipient = '[Messages] Get Recipient',
  GetRecipientSuccess = '[Messages] Get Recipient Success',
  GetRecipientFail = '[Messages] Get Recipient Fail',
  GetMessages = '[Messages] Get Messages',
  GetMessagesSuccess = '[Messages] Get Messages Success',
  GetMessagesFail = '[Messages] Get Messages Fail',
  SendMessage = '[Messages] Send Message',
  SendMessageSuccess = '[Messages] Send Message Success',
  SendMessageFail = '[Messages] Send Message Fail',
  ClearMessages = '[Messages] Clear Messages',
}

export const getRecipient = createAction(MessagesActions.GetRecipient, props<{ payload: GetRecipientPayload }>());

export const getRecipientSuccess = createAction(MessagesActions.GetRecipientSuccess, props<{ payload: GetRecipientSuccessPayload }>());

export const getRecipientFail = createAction(MessagesActions.GetRecipientFail);

export const getMessages = createAction(MessagesActions.GetMessages, props<{ payload: GetMessagesPayload }>());

export const getMessagesSuccess = createAction(MessagesActions.GetMessagesSuccess, props<{ payload: GetMessagesSuccessPayload }>());

export const getMessagesFail = createAction(MessagesActions.GetMessagesFail);

export const sendMessage = createAction(MessagesActions.SendMessage, props<{ payload: SendMessagePayload }>());

export const sendMessageSuccess = createAction(MessagesActions.SendMessageSuccess);

export const sendMessageFail = createAction(MessagesActions.SendMessageFail);

export const clearMessages = createAction(MessagesActions.ClearMessages);
