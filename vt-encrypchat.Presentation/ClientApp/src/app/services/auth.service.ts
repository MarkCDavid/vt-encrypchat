import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {API} from '../shared/constants/api.const';
import {catchError, map, tap} from 'rxjs/operators';
import {Observable, of} from 'rxjs';
import {ROUTES} from '../shared/constants/routes.const';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  private static handleError(httpError: HttpErrorResponse): GeneralError {
    const errorModel = httpError.error as GeneralError;

    if (errorModel != null && errorModel.exception) {
      throw errorModel;
    }

    return errorModel;
  }

  public SignIn(signInData: SignInRequest): Observable<SignInResponse> {
    const url = `${API.Prefix}/${API.SignIn}`;
    const options = {headers: {'Content-Type': 'application/json'}};
    const body = JSON.stringify(signInData);

    console.log(url);

    return this.http.post(url, body, options).pipe(
      map(() => {
        return { success: true } as SignInResponse;
      }),
      catchError(httpError => {
        const errorModel = AuthService.handleError(httpError);
        return of({ success: false, error: errorModel.error } as SignInResponse);
      })
    );
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
        const errorModel = AuthService.handleError(httpError);
        return of({ success: false, error: errorModel.error } as SignUpResponse);
      })
    );
  }

  public SignOut(): Observable<SignOutResponse> {
    const url = `${API.Prefix}/${API.SignOut}`;

    return this.http.post(url, null).pipe(
      map(() => {
       return { routeTo: ROUTES.HomeRedirect } as SignOutResponse;
      }),
      catchError(httpError => {
        AuthService.handleError(httpError);
        return of({ routeTo: ROUTES.HomeRedirect } as SignOutResponse);
      })
    );
  }

  public IsAuthenticated(): Observable<boolean> {
    return this.http.get(`${API.Prefix}/${API.Authenticated}`)
      .pipe(
        map(() => true),
        catchError(() => of(false)
      ));
  }
}
