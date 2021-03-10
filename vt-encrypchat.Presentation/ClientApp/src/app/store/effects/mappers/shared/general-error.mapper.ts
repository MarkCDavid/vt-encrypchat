import {GeneralErrorPayload} from '../../../actions/payloads/shared/general-error.payload';
import {GeneralError} from '../../../../models/general-error';

export function mapGeneralError(generalError: GeneralError): GeneralErrorPayload {
  return {
    generalError: generalError,
  };
}
