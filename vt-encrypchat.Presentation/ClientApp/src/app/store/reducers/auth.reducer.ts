import {Action, ActionReducer, createReducer, on} from '@ngrx/store';
import {
  checkAuthentication, checkAuthenticationFail,
  checkAuthenticationSuccess,
  signIn,
  signInFail,
  signInSuccess,
  signOutSuccess,
  signUp,
  signUpFail,
  signUpSuccess
} from '../actions';

export interface State {
  signInError?: GeneralError;
  signUpError?: GeneralError;
  userAuthenticated: boolean;
}

export const initialState: State = {
  userAuthenticated: false
};

const reducer: ActionReducer<State> = createReducer(
  initialState,
  on(signIn, (state) => ({
    ...state,
    signInError: undefined,
  })),
  on(signInSuccess, (state) => ({
    ...state,
    userAuthenticated: true,
    signInError: undefined,
  })),
  on(signInFail, (state, { errors }) => ({
    ...state,
    signInError: errors,
  })),
  on(signUp, (state) => ({
    ...state,
    signUpError: undefined,
  })),
  on(signUpSuccess, (state) => ({
    ...state,
    signUpError: undefined,
  })),
  on(signUpFail, (state, { errors }) => ({
    ...state,
    signUpError: errors,
  })),
  on(signOutSuccess, (state) => ({
    ...state,
    userAuthenticated: false,
  })),
  on(checkAuthentication, (state) => ({
    ...state
  })),
  on(checkAuthenticationSuccess, (state) => ({
    ...state,
    userAuthenticated: true,
  })),
  on(checkAuthenticationFail, (state) => ({
    ...state,
    userAuthenticated: false,
  }))
);

export function authReducer(state: State | undefined, action: Action): State {
  return reducer(state, action);
}
