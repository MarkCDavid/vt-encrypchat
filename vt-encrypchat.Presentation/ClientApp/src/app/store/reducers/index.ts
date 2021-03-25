import { ActionReducerMap } from '@ngrx/store';
import { authReducer, State as AuthState } from './auth.reducer';
import { userReducer, State as UserState } from './user.reducer';
import { messagesReducer, State as MessageState } from './messages.reducer';

export {State as AuthState} from './auth.reducer';
export {State as UserState} from './user.reducer';
export {State as MessageState} from './messages.reducer';

export interface State {
  auth: AuthState;
  user: UserState;
  messages: MessageState;
}

export const reducers: ActionReducerMap<State> = {
  auth: authReducer,
  user: userReducer,
  messages: messagesReducer,
};
