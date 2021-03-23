import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {getSignInErrors, getSignInHasErrors} from '../../store/selectors';
import {Observable} from 'rxjs';
import {Store} from '@ngrx/store';
import {signIn} from '../../store/actions';
import {SignUpRequest} from '../../services/models/auth/sign-up.model';
import {SignInPayload} from '../../store/actions/payloads/auth/sign-in.payload';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {

  public hasErrors$!: Observable<boolean>;
  public errors$!: Observable<string>;
  public signInForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private store: Store<{}>
  ) { }

  ngOnInit() {
    this.hasErrors$ = this.store.select(getSignInHasErrors);
    this.errors$ = this.store.select(getSignInErrors);
    this.signInForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(32)]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(32)]],
      gpgkey: [undefined, [Validators.required]]
    }, { updateOn: 'blur'});
  }

  onSignIn() {
    this.signInForm.markAsDirty();
    if (!this.signInForm.valid) {
      return;
    }

    const formData = this.signInForm.getRawValue();
    const request = { username: formData.username, password: formData.password } as SignUpRequest;
    const gpgKeyFile = formData.gpgkey._files[0] as File;

    const reader = new FileReader();
    reader.addEventListener('load', (event: ProgressEvent) => {
      const fileReader = event.target as FileReader;
      const gpgKey = fileReader.result as string;
      const payload = { request: request, privateKey: gpgKey } as SignInPayload;
      this.store.dispatch(signIn({ payload: payload }));
    });
    reader.readAsText(gpgKeyFile);
  }

  hasError(controlName: string, errorName: string) {
    const control = this.signInForm.controls[controlName];
    if (control.errors === null) {
      return true;
    }

    return control.errors.hasOwnProperty(errorName);
  }


}
