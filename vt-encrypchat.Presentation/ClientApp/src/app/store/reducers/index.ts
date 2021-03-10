import { ActionReducerMap } from '@ngrx/store';
import { authReducer, State as AuthState } from './auth.reducer';
import { userReducer, State as UserState } from './user.reducer';

export {State as AuthState} from './auth.reducer';
export {State as UserState} from './user.reducer';

export interface State {
  auth: AuthState;
  user: UserState;
}

export const reducers: ActionReducerMap<State> = {
  auth: authReducer,
  user: userReducer,
};
