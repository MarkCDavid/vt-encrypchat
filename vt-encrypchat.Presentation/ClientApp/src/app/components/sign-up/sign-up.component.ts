import { Component, OnInit } from '@angular/core';
import { SignUpServiceService } from '../../services/sign-up-service.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {Router} from '@angular/router';
import {MatSnackBar} from '@angular/material';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  signUpForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private signUpService: SignUpServiceService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit() {
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

    this.signUpService.SignUp(this.signUpForm.getRawValue()).subscribe(value => {
      if (value.success) {
        this.router.navigate(['/signIn']).then(r => r );
      } else {
        this.snackBar.open(value.error, 'Close', {
          duration: 10000,
          verticalPosition: 'top',
          horizontalPosition: 'center',
          panelClass: 'red-snackbar'
        });
      }
    });


  }

  hasError(controlName: string, errorName: string) {
    const control = this.signUpForm.controls[controlName];
      if (control.errors === null) {
      return true;
    }

    return control.errors.hasOwnProperty(errorName);
  }


}
