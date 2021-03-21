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
        console.log('Set User GPG');
        localStorage.setItem(LOCALSTORE.GPGKEY, gpgKey);
      } else {
        console.log('Remove User GPG');
        localStorage.removeItem(LOCALSTORE.GPGKEY);
      }
    });

    this.store.select(getUserId).pipe(skip(1)).subscribe(userId => {
      if (userId !== undefined) {
        console.log('Set User ID');
        localStorage.setItem(LOCALSTORE.USERID, userId);
      } else {
        console.log('Remove User ID');
        localStorage.removeItem(LOCALSTORE.USERID);
      }
    });

    const payload = {
      gpgKey: localStorage.getItem(LOCALSTORE.GPGKEY),
      userId: localStorage.getItem(LOCALSTORE.USERID)
    } as CheckAuthenticationPayload;

    console.log(`LocalStorage values\nUserId: ${payload.userId}\nGPG present: ${payload.gpgKey !== undefined}`);
    this.store.dispatch(checkAuthentication( { payload: payload }));
  }
}
