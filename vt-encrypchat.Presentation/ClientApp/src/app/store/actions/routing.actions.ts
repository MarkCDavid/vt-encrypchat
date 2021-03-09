import { createAction, props } from '@ngrx/store';
import { ROUTES } from 'src/app/shared/constants/routes.const';

export enum RoutingActions {
  Go = '[Routing] Go',
}

export const go = createAction(RoutingActions.Go, props<{ path: ROUTES }>());
