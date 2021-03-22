import {Component, OnInit} from '@angular/core';
import {Store} from '@ngrx/store';
import {checkAuthentication} from './store/actions';
import {LOCALSTORE} from './shared/constants/local-storage.const';
import {CheckAuthenticationPayload} from './store/actions/payloads/auth/check-authentication.payload';
import {getPrivateGPGKey, getUserId} from './store/selectors';
import {skip} from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';

  constructor(private store: Store<{}>) {
  }

  ngOnInit(): void {

    this.store.select(getPrivateGPGKey).pipe(skip(1)).subscribe(gpgKey => {
      if (gpgKey !== undefined) {
        localStorage.setItem(LOCALSTORE.GPGKEY, gpgKey);
      } else {
        localStorage.removeItem(LOCALSTORE.GPGKEY);
      }
    });

    this.store.select(getUserId).pipe(skip(1)).subscribe(userId => {
      if (userId !== undefined) {
        localStorage.setItem(LOCALSTORE.USERID, userId);
      } else {
        localStorage.removeItem(LOCALSTORE.USERID);
      }
    });

    const payload = {
      gpgKey: localStorage.getItem(LOCALSTORE.GPGKEY),
      userId: localStorage.getItem(LOCALSTORE.USERID)
    } as CheckAuthenticationPayload;

    this.store.dispatch(checkAuthentication( { payload: payload }));
  }
}
