import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {API} from '../shared/constants/api.const';
import {catchError, map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SignUpServiceService {

  constructor(private http: HttpClient) {
  }

  public SignUp(signUpData: SignUpRequest): Observable<SignUpResponse> {
    const url = `${API.Prefix}/${API.SignUp}`;
    const options = {headers: {'Content-Type': 'application/json'}};
    const body = JSON.stringify(signUpData);

    return this.http.post(url, body, options).pipe(
      map(() => {
        return { success: true } as SignUpResponse;
      }),
      catchError(httpError => {
        const errorModel = httpError.error as Error;

        if (errorModel.exception) {
          throw errorModel;
        }

        return of({ success: false, error: errorModel.error } as SignUpResponse);
      })
    );
  }
}

