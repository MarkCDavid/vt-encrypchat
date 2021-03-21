import { createAction, props } from '@ngrx/store';

export enum ToastsActions {
  OK = '[Toasts] OK',
  Error = '[Toasts] Error',
}

export const toastOK = createAction(ToastsActions.OK, props<{ message: string }>());
export const toastError = createAction(ToastsActions.Error, props<{ message: string }>());
