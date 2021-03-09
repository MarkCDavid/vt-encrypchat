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
  signOut,
  signOutSuccess,
  signUp,
  signUpFail,
  signUpSuccess
} from '../actions';
import {tap} from 'rxjs/operators';
import {ROUTES} from '../../shared/constants/routes.const';

@Injectable()
export class AuthEffects {
  constructor(
    private actions$: Actions,
    private store: Store,
    private authService: AuthService,
  ) {
  }

  public signUpUser$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(signUp),
        tap(({payload}) => {
          this.authService.SignUp(payload).subscribe(
            (signUpResponse: SignUpResponse) => {
              if (signUpResponse.success) {
                this.store.dispatch(signUpSuccess({payload: signUpResponse}));
              } else {
                this.store.dispatch(signUpFail({errors: {error: signUpResponse.error}}));
              }
            }
          );
        })
      ),
    {dispatch: false}
  );
  public signInUser$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(signIn),
        tap(({payload}) => {
          this.authService.SignIn(payload).subscribe(
            (signInResponse: SignInResponse) => {
              if (signInResponse.success) {
                this.store.dispatch(signInSuccess({payload: signInResponse}));
                this.store.dispatch(go({ path: ROUTES.Home }));
              } else {
                this.store.dispatch(signInFail({errors: {error: signInResponse.error}}));
              }
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
        tap(() => {
          this.authService.IsAuthenticated().subscribe(
            authenticated => {
              if (authenticated) {
                this.store.dispatch(checkAuthenticationSuccess());
              } else {
                this.store.dispatch(checkAuthenticationFail());
                this.store.dispatch(go({ path: ROUTES.SignIn }));
              }
            }
          );
        })
      ),
    {dispatch: false}
  );
}
