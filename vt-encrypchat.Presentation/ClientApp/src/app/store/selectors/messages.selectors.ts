import {createFeatureSelector, createSelector} from '@ngrx/store';
import {MessageState} from '../reducers';


export const getMessagesState = createFeatureSelector<MessageState>('messages');

export const getCurrentMessages = createSelector(getMessagesState, (state) => state.messages);

export const getSending = createSelector(getMessagesState, (state) => state.sending);

export const getSent = createSelector(getMessagesState, (state) => state.sent);
