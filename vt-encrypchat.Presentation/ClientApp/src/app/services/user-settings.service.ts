import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {API} from '../shared/constants/api.const';
import {catchError, map} from 'rxjs/operators';
import {handleError} from '../shared/handlers/http-error-handler';
import {GetUserSettingsRequest, GetUserSettingsResponse} from './models/user/get-user-settings.model';

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
}
