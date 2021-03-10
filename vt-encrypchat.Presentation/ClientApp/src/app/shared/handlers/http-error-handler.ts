import {HttpErrorResponse} from '@angular/common/http';
import {GeneralError} from '../../models/general-error';

export function handleError(httpError: HttpErrorResponse): GeneralError {
  const errorModel = httpError.error as GeneralError;

  if (errorModel != null && errorModel.exception) {
    throw errorModel;
  }

  return errorModel;
}
