import {Component, OnInit} from '@angular/core';
import {Store} from '@ngrx/store';
import {getFoundUsers} from '../../store/selectors';
import {Observable} from 'rxjs';
import {User} from '../../models/user';
import {FormControl} from '@angular/forms';
import {debounceTime} from 'rxjs/operators';
import {GetUsersPayload} from '../../store/actions/payloads/user/get-users.payload';
import {GetUsersRequest} from '../../services/models/user/get-users.model';
import {getUsers} from '../../store/actions';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public users$!: Observable<User[]>;
  public search = new FormControl('');

  constructor(private store: Store<{}>) { }

  ngOnInit(): void {
    this.users$ = this.store.select(getFoundUsers);

    this.search.valueChanges.pipe(debounceTime(1000)).subscribe(value => {
      const request = { search: value } as GetUsersRequest;
      const payload = { request: request } as GetUsersPayload;
      this.store.dispatch(getUsers({ payload: payload }));
    });

    const request = { search: '' } as GetUsersRequest;
    const payload = { request: request } as GetUsersPayload;
    this.store.dispatch(getUsers({ payload: payload }));
  }


}
