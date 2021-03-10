import {Component, OnInit} from '@angular/core';
import {Store} from '@ngrx/store';
import {checkAuthentication} from './store/actions';
import {LOCALSTORE} from './shared/constants/local-storage.const';
import {CheckAuthenticationPayload} from './store/actions/payloads/auth/check-authentication.payload';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';

  constructor(private store: Store<{}>) {
  }

  ngOnInit(): void {
    const gpgKey = localStorage.getItem(LOCALSTORE.GPGKEY);
    const payload = { gpgKey: gpgKey } as CheckAuthenticationPayload;
    this.store.dispatch(checkAuthentication( { payload: payload }));
  }
}
