import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {Store} from '@ngrx/store';
import {getIsAuthenticated, getPrivateGPGKey, getUserId} from '../store/selectors';
import {checkAuthentication} from '../store/actions';
import {CheckAuthenticationPayload} from '../store/actions/payloads/auth/check-authentication.payload';
import {skipWhile} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  public gpgKey$: Observable<string | undefined>;
  public userId$: Observable<string | undefined>;

  constructor(private store: Store<{}>) {
    this.gpgKey$ = this.store.select(getPrivateGPGKey);
    this.userId$ = this.store.select(getUserId);
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    this.gpgKey$.pipe(skipWhile(value => value === undefined)).subscribe(gpgKey => {
      this.userId$.pipe(skipWhile(value => value === undefined)).subscribe(userId => {
        const payload = {
          gpgKey: gpgKey,
          userId: userId,
        } as CheckAuthenticationPayload;
        this.store.dispatch(checkAuthentication( { payload: payload }));
      });
    });
    return this.store.select(getIsAuthenticated);
  }
}
