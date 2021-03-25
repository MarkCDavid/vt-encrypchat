import {createFeatureSelector, createSelector} from '@ngrx/store';
import {MessageState} from '../reducers';
import {getPrivateGPGKey, getUserId} from "./auth.selectors";
import {getPublicGPGKey} from "./user.selectors";
import {User} from "../../models/user";
import {MessagesRequestDataModel} from "./models/messages-request-data.model";
import {Message} from "../../models/message";
import {MessagesPollingDataModel} from "./models/messages-polling-data.model";


export const getMessagesState = createFeatureSelector<MessageState>('messages');

export const getCurrentRecipient = createSelector(getMessagesState, (state) => state.recipient);

export const getCurrentMessages = createSelector(getMessagesState, (state) => state.messages);

export const getSending = createSelector(getMessagesState, (state) => state.sending);

export const getSent = createSelector(getMessagesState, (state) => state.sent);

export const getMessagesRequestData = createSelector(
  getUserId,
  getPublicGPGKey,
  getPrivateGPGKey,
  getCurrentRecipient,
  (
    userId: string | undefined,
    publicKey: string,
    privateKey: string | undefined,
    currentRecipient: User | undefined) => {
    return {
      userId: userId,
      publicKey: publicKey,
      privateKey: privateKey,
      currentRecipient: currentRecipient
    } as MessagesRequestDataModel;
  }
);

export const getMessagesPollingData = createSelector(
  getUserId,
  getCurrentMessages,
  getCurrentRecipient,
  (
    userId: string | undefined,
    messages: Message[],
    currentRecipient: User | undefined) => {
    return {
      userId: userId,
      lastMessage: messages.slice(-1).pop(),
      currentRecipient: currentRecipient
    } as MessagesPollingDataModel;
  }
);

