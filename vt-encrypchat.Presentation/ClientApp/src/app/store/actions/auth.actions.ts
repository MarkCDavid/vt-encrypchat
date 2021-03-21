import {createAction, props} from '@ngrx/store';
import {SignInPayload, SignInSuccessPayload} from './payloads/auth/sign-in.payload';
import {SignUpPayload} from './payloads/auth/sign-up.payload';
import {GeneralErrorPayload} from './payloads/shared/general-error.payload';
import {CheckAuthenticationPayload, CheckAuthenticationSuccessPayload} from './payloads/auth/check-authentication.payload';

export enum AuthActions {
  CheckAuthentication = '[Auth] Check Authentication',
  CheckAuthenticationSuccess = '[Auth] Check Authentication Success',
  CheckAuthenticationFail = '[Auth] Check Authentication Fail',
  SignIn = '[Auth] Sign in',
  SignInSuccess = '[Auth] Sign in success',
  SignInFail = '[Auth] Sign in fail',
  SignUp = '[Auth] Sign up',
  SignUpSuccess = '[Auth] Sign up success',
  SignUpFail = '[Auth] Sign up fail',
  SignOut = '[Auth] Sign out',
  SignOutSuccess = '[Auth] Sign out success',
  SignOutFail = '[Auth] Sign out fail',
}

export const checkAuthentication = createAction(AuthActions.CheckAuthentication, props<{ payload: CheckAuthenticationPayload }>());

export const checkAuthenticationSuccess = createAction(
  AuthActions.CheckAuthenticationSuccess, props<{ payload: CheckAuthenticationSuccessPayload }>());

export const checkAuthenticationFail = createAction(AuthActions.CheckAuthenticationFail);


export const signIn = createAction(AuthActions.SignIn, props<{ payload: SignInPayload }>());

export const signInSuccess = createAction(AuthActions.SignInSuccess, props<{ payload: SignInSuccessPayload }>());

export const signInFail = createAction(AuthActions.SignInFail, props<{ payload: GeneralErrorPayload }>());


export const signUp = createAction(AuthActions.SignUp, props<{ payload: SignUpPayload }>());

export const signUpSuccess = createAction(AuthActions.SignUpSuccess);

export const signUpFail = createAction(AuthActions.SignUpFail, props<{ payload: GeneralErrorPayload }>());


export const signOut = createAction(AuthActions.SignOut);

export const signOutSuccess = createAction(AuthActions.SignOutSuccess);
