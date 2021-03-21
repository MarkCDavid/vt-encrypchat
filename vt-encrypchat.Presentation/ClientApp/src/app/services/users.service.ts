import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {GetUserSettingsRequest, GetUserSettingsResponse} from './models/user/get-user-settings.model';
import {Observable, throwError} from 'rxjs';
import {API} from '../shared/constants/api.const';
import {catchError, map} from 'rxjs/operators';
import {handleError} from '../shared/handlers/http-error-handler';
import {GetUsersRequest, GetUsersResponse} from './models/user/get-users.model';
import {User} from "../models/user";

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) { }

  public GetUsers(getUsersRequest: GetUsersRequest): Observable<GetUsersResponse> {
    const url = `${API.Prefix}/${API.Users}`;
    const options = { params: { search: getUsersRequest.search } };
    return this.http.get(url, options).pipe(
      map((response: Object) => {
        return { users: response } as GetUsersResponse
      }),
      catchError(httpError => throwError(handleError(httpError)) )
    );
  }
}
