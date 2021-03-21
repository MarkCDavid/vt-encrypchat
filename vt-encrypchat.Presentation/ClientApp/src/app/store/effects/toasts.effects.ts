import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { tap } from 'rxjs/operators';
import {go, toastError, toastOK} from '../actions';
import {MatSnackBar} from '@angular/material';

@Injectable()
export class ToastsEffects {
  constructor(private actions$: Actions, private snackBar: MatSnackBar) {}

  public showToastOK$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(toastOK),
        tap(({ message }) => {
          this.snackBar.open( message, 'OK', { duration: 10000, panelClass: ['alert-ok'], });
        })
      ),
    { dispatch: false }
  );

  public showToastError$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(toastError),
        tap(({ message }) => {
          this.snackBar.open( message, 'OK', { duration: 10000, panelClass: ['alert-error'], });
        })
      ),
    { dispatch: false }
  );
}
