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
import {GeneralError} from '../../models/general-error';

export interface State {
  signInError?: GeneralError;
  signUpError?: GeneralError;
  userId?: string;
  userAuthenticated: boolean;
  privateGPGKey?: string;
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
  on(signInSuccess, (state, { payload }) => ({
    ...state,
    userAuthenticated: true,
    signInError: undefined,
    userId: payload.userId,
    privateGPGKey: payload.gpgKey
  })),
  on(signInFail, (state, { payload }) => ({
    ...state,
    signInError: payload.generalError,
    privateGPGKey: undefined
  })),



  on(signUp, (state) => ({
    ...state,
    signUpError: undefined,
  })),
  on(signUpSuccess, (state) => ({
    ...state,
    signUpError: undefined,
  })),
  on(signUpFail, (state, { payload }) => ({
    ...state,
    signUpError: payload.generalError,
  })),


  on(signOutSuccess, (state) => ({
    ...state,
    userAuthenticated: false,
    privateGPGKey: undefined
  })),


  on(checkAuthentication, (state) => ({
    ...state,
  })),
  on(checkAuthenticationSuccess, (state, { payload }) => ({
    ...state,
    userAuthenticated: true,
    privateGPGKey: payload.gpgKey
  })),
  on(checkAuthenticationFail, (state) => ({
    ...state,
    userAuthenticated: false,
    privateGPGKey: undefined,
  }))
);

export function authReducer(state: State | undefined, action: Action): State {
  return reducer(state, action);
}
