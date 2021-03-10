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
  userPGPKey?: string;
}

export const initialState: State = {
  userAuthenticated: false
};

const reducer: ActionReducer<State> = createReducer(
  initialState,
  on(signIn, (state) => ({
    ...state,
    signInError: undefined,
    userPGPKey: undefined
  })),
  on(signInSuccess, (state, { pgpKey }) => ({
    ...state,
    userAuthenticated: true,
    signInError: undefined,
    userPGPKey: pgpKey
  })),
  on(signInFail, (state, { errors }) => ({
    ...state,
    signInError: errors,
    userPGPKey: undefined
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
    userPGPKey: undefined
  })),
  on(checkAuthentication, (state) => ({
    ...state,
    userPGPKey: undefined
  })),
  on(checkAuthenticationSuccess, (state, { pgpKey }) => ({
    ...state,
    userAuthenticated: true,
    userPGPKey: pgpKey
  })),
  on(checkAuthenticationFail, (state) => ({
    ...state,
    userAuthenticated: false,
    userPGPKey: undefined,
  }))
);

export function authReducer(state: State | undefined, action: Action): State {
  return reducer(state, action);
}
