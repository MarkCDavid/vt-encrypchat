import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {getSignInErrors, getSignInHasErrors, getSignUpErrors, getSignUpHasErrors} from '../../store/selectors';
import {Observable} from 'rxjs';
import {Store} from '@ngrx/store';
import {signIn, signUp} from '../../store/actions';
import {ROUTES} from '../../shared/constants/routes.const';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {

  public hasErrors$: Observable<boolean>;
  public errors$: Observable<string>;
  public signInForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private store: Store<{}>
  ) { }

  ngOnInit() {
    this.hasErrors$ = this.store.select(getSignInHasErrors);
    this.errors$ = this.store.select(getSignInErrors);
    this.signInForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(32)]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(32)]]
    }, { updateOn: 'blur'});
  }

  onSignIn() {
    this.signInForm.markAsDirty();
    if (!this.signInForm.valid) {
      return;
    }

    this.store.dispatch(signIn({ payload: this.signInForm.getRawValue() as SignInRequest }));
  }

  hasError(controlName: string, errorName: string) {
    const control = this.signInForm.controls[controlName];
    if (control.errors === null) {
      return true;
    }

    return control.errors.hasOwnProperty(errorName);
  }
}
