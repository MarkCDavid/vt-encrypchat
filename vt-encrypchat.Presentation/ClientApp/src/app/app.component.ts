import {Component, OnInit} from '@angular/core';
import {Store} from '@ngrx/store';
import {checkAuthentication} from './store/actions';
import {LOCALSTORE} from './shared/constants/local-storage.const';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {

  title = 'app';


  constructor(private store: Store<{}>) {
  }

  ngOnInit(): void {
    const pgpKey = localStorage.getItem(LOCALSTORE.PGPKEY);
    this.store.dispatch(checkAuthentication( { pgpKey: pgpKey }));
  }
}
