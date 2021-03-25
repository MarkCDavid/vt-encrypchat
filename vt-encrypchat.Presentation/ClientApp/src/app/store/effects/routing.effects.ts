import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { tap } from 'rxjs/operators';
import {go, goExtras} from '../actions';

@Injectable()
export class RoutingEffects {
  constructor(private actions$: Actions, private router: Router) {}

  public goToSpecificRoute$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(go),
        tap(async ({ path }) => {
          await this.router.navigate([path]);
        })
      ),
    { dispatch: false }
  );

  public goToSpecificRouteWithExtras$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(goExtras),
        tap(async ({ path, extras }) => {
          await this.router.navigate([path], extras);
        })
      ),
    { dispatch: false }
  );
}
