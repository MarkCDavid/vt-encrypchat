import { ActionReducerMap } from '@ngrx/store';
import { authReducer, State as AuthState } from './auth.reducer';

export {State as AuthState} from './auth.reducer';

export interface State {
  auth: AuthState;
}

export const reducers: ActionReducerMap<State> = {
  auth: authReducer,
};
