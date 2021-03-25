import {Component, OnInit} from '@angular/core';
import {ROUTES} from '../shared/constants/routes.const';
import {Router} from '@angular/router';
import {Store} from '@ngrx/store';
import {Observable} from 'rxjs';
import {getDisplayName, getIsAuthenticated, getUserState} from '../store/selectors';
import {go, signOut} from '../store/actions';
import {User} from "../models/user";
import {UserState} from "../store/reducers";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  public isAuthenticated$!: Observable<boolean>;
  public displayName$!: Observable<string>;

  constructor(
    private router: Router,
    private store: Store<{}>) {
  }

  ngOnInit(): void {
    this.isAuthenticated$ = this.store.select(getIsAuthenticated);
    this.displayName$ = this.store.select(getDisplayName);
  }

  Home() {
    this.store.dispatch(go({ path: ROUTES.Home }));
  }

  SignUp() {
    this.store.dispatch(go({ path: ROUTES.SignUp }));
  }

  SignIn() {
    this.store.dispatch(go({ path: ROUTES.SignIn }));
  }

  Settings() {
    this.store.dispatch(go({ path: ROUTES.UserSettings }));
  }

  SignOut() {
    this.store.dispatch(signOut());
  }
}
