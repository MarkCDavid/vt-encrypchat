import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {Store} from '@ngrx/store';
import {signUp} from '../../store/actions';
import {Observable} from 'rxjs';
import {getSignUpErrors, getSignUpHasErrors} from '../../store/selectors';
import {SignUpRequest} from '../../services/models/auth/sign-up.model';
import {SignUpPayload} from '../../store/actions/payloads/auth/sign-up.payload';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  public hasErrors$!: Observable<boolean>;
  public errors$!: Observable<string>;
  public signUpForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private store: Store<{}>
  ) { }

  ngOnInit() {

    this.hasErrors$ = this.store.select(getSignUpHasErrors);
    this.errors$ = this.store.select(getSignUpErrors);

    this.signUpForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(32)]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(32)]]
    }, { updateOn: 'blur'});
  }

  onSignUp() {
    this.signUpForm.markAsDirty();
    if (!this.signUpForm.valid) {
      return;
    }

    const formData = this.signUpForm.getRawValue();
    const request = { username: formData.username, password: formData.password } as SignUpRequest;
    const payload = { request: request} as SignUpPayload;
    this.store.dispatch(signUp({ payload: payload }));
  }

  hasError(controlName: string, errorName: string) {
    const control = this.signUpForm.controls[controlName];
      if (control.errors === null) {
      return true;
    }

    return control.errors.hasOwnProperty(errorName);
  }
}
