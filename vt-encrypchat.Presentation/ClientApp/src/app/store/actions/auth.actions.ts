import {createAction, props} from '@ngrx/store';

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
}

export const checkAuthentication = createAction(AuthActions.CheckAuthentication);

export const checkAuthenticationSuccess = createAction(AuthActions.CheckAuthenticationSuccess);

export const checkAuthenticationFail = createAction(AuthActions.CheckAuthenticationFail);

export const signIn = createAction(AuthActions.SignIn, props<{ payload: SignInRequest }>());

export const signInSuccess = createAction(AuthActions.SignInSuccess, props<{ payload: SignInResponse }>());

export const signInFail = createAction(AuthActions.SignInFail, props<{ errors: GeneralError }>());

export const signUp = createAction(AuthActions.SignUp, props<{ payload: SignUpRequest }>());

export const signUpSuccess = createAction(AuthActions.SignUpSuccess, props<{ payload: SignUpResponse }>());

export const signUpFail = createAction(AuthActions.SignUpFail, props<{ errors: GeneralError }>());

export const signOut = createAction(AuthActions.SignOut);

export const signOutSuccess = createAction(AuthActions.SignOutSuccess);
