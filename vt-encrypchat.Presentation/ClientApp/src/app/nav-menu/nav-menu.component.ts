import {Component, OnInit} from '@angular/core';
import {ROUTES} from '../shared/constants/routes.const';
import {Router} from '@angular/router';
import {Store} from '@ngrx/store';
import {Observable} from 'rxjs';
import {getIsAuthenticated} from '../store/selectors';
import {go, signOut} from '../store/actions';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  public isAuthenticated$!: Observable<boolean>;

  constructor(
    private router: Router,
    private store: Store<{}>) {
  }

  ngOnInit(): void {
    this.isAuthenticated$ = this.store.select(getIsAuthenticated);
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
