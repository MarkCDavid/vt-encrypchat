import {Injectable, OnInit} from '@angular/core';
import {AuthService} from './auth.service';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import {ROUTES} from '../shared/constants/routes.const';
import {Observable} from 'rxjs';
import {map, tap} from 'rxjs/operators';
import {FormBuilder} from '@angular/forms';
import {Store} from '@ngrx/store';
import {getIsAuthenticated, getSignUpHasErrors} from '../store/selectors';
import {checkAuthentication, signUp} from '../store/actions';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements OnInit, CanActivate {

  public isAuthenticated$: Observable<boolean>;

  constructor(private store: Store<{}>) { }

  ngOnInit(): void {
    this.isAuthenticated$ = this.store.select(getIsAuthenticated);
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    this.store.dispatch(checkAuthentication());
    return this.isAuthenticated$;
  }
}
