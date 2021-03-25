import {Action, ActionReducer, createReducer, on} from '@ngrx/store';
import {
  getMessages,
  getMessagesFail,
  getMessagesSuccess,
  getRecipient,
  getRecipientSuccess,
  sendMessage,
  sendMessageFail,
  sendMessageSuccess,
} from '../actions';
import {User} from '../../models/user';
import {Message} from "../../models/message";

export interface State {
  recipient?: User;
  messages: Message[];
  sending: boolean;
  sent: boolean;
}

export const initialState: State = {
  messages: [],
  sending: false,
  sent: false,
};

const reducer: ActionReducer<State> = createReducer(
  initialState,
  on(getRecipient, (state) => ({
    ...state,
  })),
  on(getRecipientSuccess, (state, { payload }) => ({
    ...state,
    recipient: payload.recipient,
  })),
  on(getMessagesFail, (state) => ({
    ...state,
    recipient: undefined,
  })),
  on(getMessages, (state) => ({
    ...state,
  })),
  on(getMessagesSuccess, (state, { payload }) => ({
    ...state,
    messages: payload.messages,
  })),
  on(getMessagesFail, (state) => ({
    ...state,
    messages: []
  })),
  on(sendMessage, (state) => ({
    ...state,
    sending: true,
    sent: false,
  })),
  on(sendMessageSuccess, (state) => ({
    ...state,
    sending: false,
    sent: true,
  })),
  on(sendMessageFail, (state) => ({
    ...state,
    sending: false,
    sent: false,
  })),
);

export function messagesReducer(state: State | undefined, action: Action): State {
  return reducer(state, action);
}
