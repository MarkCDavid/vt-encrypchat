import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {Store} from '@ngrx/store';
import {getIsAuthenticated, getPrivateGPGKey} from '../store/selectors';
import {checkAuthentication} from '../store/actions';
import {CheckAuthenticationPayload} from '../store/actions/payloads/auth/check-authentication.payload';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  public gpgKey$: Observable<string | undefined>;

  constructor(private store: Store<{}>) {
    this.gpgKey$ = this.store.select(getPrivateGPGKey);
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    this.gpgKey$.subscribe(gpgKey => {
      const payload = { gpgKey: gpgKey} as CheckAuthenticationPayload;
      this.store.dispatch(checkAuthentication( { payload: payload }));
    });
    return this.store.select(getIsAuthenticated);
  }
}
