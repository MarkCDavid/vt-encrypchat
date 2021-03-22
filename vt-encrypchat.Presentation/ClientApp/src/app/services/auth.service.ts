import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {API} from '../shared/constants/api.const';
import {catchError, map} from 'rxjs/operators';
import {Observable, of, throwError} from 'rxjs';
import {handleError} from '../shared/handlers/http-error-handler';
import {SignInRequest, SignInResponse} from './models/auth/sign-in.model';
import {SignUpRequest} from './models/auth/sign-up.model';
import {CryptoService} from "./crypto.service";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient, private crypto: CryptoService) { }

  public SignIn(signInRequest: SignInRequest): Observable<SignInResponse> {
    const url = `${API.Prefix}/${API.SignIn}`;
    const options = {headers: {'Content-Type': 'application/json'}};
    const body = JSON.stringify({
      ...signInRequest,
      password: this.crypto.hash(signInRequest.password)
    });

    return this.http.post(url, body, options).pipe(
      map((response: Object) => response as SignInResponse),
      catchError(httpError => throwError(handleError(httpError)) )
    );
  }

  public SignUp(signUpRequest: SignUpRequest): Observable<void> {
    const url = `${API.Prefix}/${API.SignUp}`;
    const options = {headers: {'Content-Type': 'application/json'}};
    const body = JSON.stringify({
      ...signUpRequest,
      password: this.crypto.hash(signUpRequest.password)
    });

    return this.http.post(url, body, options).pipe(
      map(() => {}),
      catchError(httpError => throwError(handleError(httpError)) )
    );
  }

  public SignOut(): Observable<void> {
    const url = `${API.Prefix}/${API.SignOut}`;

    return this.http.post(url, null).pipe(
      map(() => {}),
      catchError(httpError => throwError(handleError(httpError)) )
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
