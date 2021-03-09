import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { tap } from 'rxjs/operators';
import { go } from '../actions';

@Injectable()
export class RoutingEffects {
  constructor(private actions$: Actions, private router: Router) {}

  public goToSpecificRoute$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(go),
        tap(({ path }) => {
          this.router.navigate([path]);
        })
      ),
    { dispatch: false }
  );
}
