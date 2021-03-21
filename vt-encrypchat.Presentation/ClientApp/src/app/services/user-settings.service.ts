import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {API} from '../shared/constants/api.const';
import {catchError, map} from 'rxjs/operators';
import {handleError} from '../shared/handlers/http-error-handler';
import {GetUserSettingsRequest, GetUserSettingsResponse} from './models/user/get-user-settings.model';
import {SetUserSettingsRequest} from './models/user/set-user-settings.model';

@Injectable({
  providedIn: 'root'
})
export class UserSettingsService {

  constructor(private http: HttpClient) { }

  public GetUserSettings(userSettingsRequest: GetUserSettingsRequest): Observable<GetUserSettingsResponse> {
    const url = `${API.Prefix}/${API.UserSettings}/${userSettingsRequest.userId}`;
    return this.http.get(url, {}).pipe(
      map((response: Object) => response as GetUserSettingsResponse ),
      catchError(httpError => throwError(handleError(httpError)) )
    );
  }

  public SetUserSettings(userSettingsRequest: SetUserSettingsRequest): Observable<void> {
    const url = `${API.Prefix}/${API.UserSettings}/${userSettingsRequest.userId}`;
    const options = {headers: {'Content-Type': 'application/json'}};
    const body = JSON.stringify(userSettingsRequest);
    return this.http.put(url, body, options).pipe(
      map(() => {}),
      catchError(httpError => throwError(handleError(httpError)) )
    );
  }
}
