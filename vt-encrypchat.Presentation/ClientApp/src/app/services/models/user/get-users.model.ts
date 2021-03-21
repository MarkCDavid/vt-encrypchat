import {User} from '../../../models/user';

export interface GetUsersRequest {
  search: string;
}

export interface GetUsersResponse {
  users: User[];
}


