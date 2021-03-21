import {GetUsersRequest} from '../../../../services/models/user/get-users.model';
import {User} from '../../../../models/user';

export interface GetUsersPayload {
  request: GetUsersRequest;
}

export interface GetUsersSuccessPayload {
  users: User[];
}
