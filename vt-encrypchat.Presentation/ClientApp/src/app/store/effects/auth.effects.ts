import {Injectable} from '@angular/core';
import {Store} from '@ngrx/store';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {AuthService} from '../../services/auth.service';
import {
  checkAuthentication, checkAuthenticationFail, checkAuthenticationSuccess, getUserSettings,
  go, loadUserPublicKey,
  signIn,
  signInFail,
  signInSuccess,
  signOut, signOutSuccess,
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
import {GetUserSettingsRequest} from "../../services/models/user/get-user-settings.model";
import {GetUserSettingsPayload} from "../actions/payloads/user/get-user-settings.payload";
import {mapLoadUserPublicKeyPayload} from "./mappers/user/load-user-public-key.mapper";

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
          this.store.dispatch(go({ path: ROUTES.Home }));
          const request = { userId: payload.userId } as GetUserSettingsRequest;
          const settingsPayload = { request: request } as GetUserSettingsPayload;
          this.store.dispatch(getUserSettings( { payload: settingsPayload } ));
        })
      ),
    {dispatch: false}
  );

  public signOutSuccess = createEffect(
    () =>
      this.actions$.pipe(
        ofType(signOutSuccess),
        tap(( ) => {
          this.store.dispatch(go({ path: ROUTES.SignIn }));
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
              this.store.dispatch(go({ path: ROUTES.Home }));
              this.store.dispatch(signOutSuccess());
            },
            () => {
              this.store.dispatch(go({ path: ROUTES.Home }));
              this.store.dispatch(signOutSuccess());
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
              const authenticatedWithoutGPG = authenticated && !payload.privateKey;
              if (!authenticated || authenticatedWithoutGPG) {
                this.store.dispatch(checkAuthenticationFail());
              } else {
                this.store.dispatch(checkAuthenticationSuccess( { payload: mapCheckAuthenticationSuccessPayload(payload) }));
                this.store.dispatch(loadUserPublicKey({ payload: mapLoadUserPublicKeyPayload(payload) }));
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
          this.store.dispatch(go({ path: ROUTES.SignIn }));
        })
      ),
    {dispatch: false}
  );
}
