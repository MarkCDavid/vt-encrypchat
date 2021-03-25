import { createAction, props } from '@ngrx/store';
import { ROUTES } from 'src/app/shared/constants/routes.const';
import {NavigationExtras, Params} from "@angular/router";

export enum RoutingActions {
  Go = '[Routing] Go',
  GoExtras = '[Routing] Go Extras',
}

export const go = createAction(RoutingActions.Go, props<{ path: ROUTES }>());

export const goExtras = createAction(RoutingActions.GoExtras, props<{ path: ROUTES, extras: NavigationExtras | undefined }>());
