import {GetUsersResponse} from '../../../../services/models/user/get-users.model';
import {GetUsersSuccessPayload} from '../../../actions/payloads/user/get-users.payload';

export function mapGetUsersSuccessPayload(response: GetUsersResponse): GetUsersSuccessPayload {
  return {
    users: response.users
  };
}


