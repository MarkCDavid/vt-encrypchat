import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Store} from '@ngrx/store';
import {getDisplayName, getPublicGPGKey} from '../../store/selectors';
import {getUserSettings} from '../../store/actions';
import {GetUserSettingsPayload} from '../../store/actions/payloads/user/get-user-settings.payload';
import {GetUserSettingsRequest} from '../../services/models/user/get-user-settings.model';
import {LOCALSTORE} from '../../shared/constants/local-storage.const';

@Component({
  selector: 'app-user-settings',
  templateUrl: './user-settings.component.html',
  styleUrls: ['./user-settings.component.css']
})
export class UserSettingsComponent implements OnInit {

  public userSettingsForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private store: Store<{}>
  ) { }

  ngOnInit() {
    this.userSettingsForm = this.formBuilder.group({
      displayName: ['', [Validators.minLength(6), Validators.maxLength(32)]],
      publicGPGKey: ['']
    }, { updateOn: 'blur'});

    this.store.select(getDisplayName).subscribe((displayName: string) => {
      const control = this.userSettingsForm.get('displayName');
      if (control == null) {
        return;
      }
      control.setValue(displayName);
    });

    this.store.select(getPublicGPGKey).subscribe((gpgKey: string | undefined) => {
      const control = this.userSettingsForm.get('publicGPGKey');
      if (control == null) {
        return;
      }
      control.setValue(gpgKey);
    });

    const userId = localStorage.getItem(LOCALSTORE.USERID);
    const request = { userId: userId } as GetUserSettingsRequest;
    const payload = { request: request } as GetUserSettingsPayload;
    this.store.dispatch(getUserSettings( { payload: payload }));
  }

  onSignUp() {
    this.userSettingsForm.markAsDirty();
    if (!this.userSettingsForm.valid) {
      return;
    }

    // this.store.dispatch(signUp({ payload: this.signUpForm.getRawValue() as SignUpRequest }));
  }

  hasError(controlName: string, errorName: string) {
    const control = this.userSettingsForm.controls[controlName];
    if (control.errors === null) {
      return true;
    }

    return control.errors.hasOwnProperty(errorName);
  }

}
