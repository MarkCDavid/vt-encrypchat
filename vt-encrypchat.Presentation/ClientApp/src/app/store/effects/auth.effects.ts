import {Injectable} from '@angular/core';
import {Store} from '@ngrx/store';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {AuthService} from '../../services/auth.service';
import {
  checkAuthentication, checkAuthenticationFail, checkAuthenticationSuccess,
  go,
  signIn,
  signInFail,
  signInSuccess,
  signOut, signOutFail,
  signOutSuccess,
  signUp,
  signUpFail,
  signUpSuccess, toastError, toastOK
} from '../actions';
import {tap} from 'rxjs/operators';
import {ROUTES} from '../../shared/constants/routes.const';
import {SignInResponse} from '../../services/models/auth/sign-in.model';
import {mapSignInSuccessPayload} from './mappers/auth/sign-in.mapper';
import {mapGeneralError} from './mappers/shared/general-error.mapper';
import {mapCheckAuthenticationSuccessPayload} from './mappers/auth/check-authentication.mapper';
import {GeneralError} from '../../models/general-error';

@Injectable()
export class AuthEffects {
  constructor(
    private actions$: Actions,
    private store: Store,
    private authService: AuthService,
  ) {
  }

  public signInUser$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(signIn),
        tap(( { payload } ) => {
          this.authService.SignIn(payload.request).subscribe(
            (response: SignInResponse) => {
              const signInSuccessPayload = mapSignInSuccessPayload(payload, response);
              this.store.dispatch(signInSuccess({ payload: signInSuccessPayload }));
              this.store.dispatch(go({ path: ROUTES.Home }));
            },
            (generalError: GeneralError) => {
              this.store.dispatch(signInFail({ payload: mapGeneralError(generalError) }));
            }
          );
        })
      ),
    {dispatch: false}
  );

  public signInUserSuccess$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(signInSuccess),
        tap(( { payload } ) => {
          this.store.dispatch(toastOK( { message: 'Signed in successfully!' }));
        })
      ),
    {dispatch: false}
  );

    public signInUserFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(signInFail),
        tap(( { payload } ) => {
          this.store.dispatch(toastError( { message: payload.generalError.error }));
        })
      ),
    {dispatch: false}
  );

  public signUpUser$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(signUp),
        tap(({ payload }) => {
          this.authService.SignUp(payload.request).subscribe(
            () => {
              this.store.dispatch(signUpSuccess());
            },
            (generalError: GeneralError) => {
              this.store.dispatch(signUpFail({payload: mapGeneralError(generalError)}));
            }
          );
        })
      ),
    {dispatch: false}
  );

  public signOutUser$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(signOut),
        tap(() => {
          this.authService.SignOut().subscribe(
            () => {
              this.store.dispatch(signOutSuccess());
              this.store.dispatch(go({ path: ROUTES.Home }));
            },
            () => {
              this.store.dispatch(signOutFail());
            }
          );
        })
      ),
    {dispatch: false}
  );

  public checkAuthentication$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(checkAuthentication),
        tap(( { payload } ) => {
          this.authService.IsAuthenticated().subscribe(
            authenticated => {
              const authenticatedWithoutGPG = authenticated && !payload.gpgKey;
              if (!authenticated || authenticatedWithoutGPG) {
                this.store.dispatch(checkAuthenticationFail());
              } else {
                this.store.dispatch(checkAuthenticationSuccess( { payload: mapCheckAuthenticationSuccessPayload(payload) }));
              }
            }
          );
        })
      ),
    {dispatch: false}
  );

  public checkAuthenticationFail$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(checkAuthenticationFail),
        tap(( ) => {
          this.store.dispatch(signOut());
          this.store.dispatch(go({ path: ROUTES.SignIn }));
        })
      ),
    {dispatch: false}
  );
}
