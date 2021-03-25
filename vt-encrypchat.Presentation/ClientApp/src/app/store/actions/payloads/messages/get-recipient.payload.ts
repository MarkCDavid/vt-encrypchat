import {GetUsersRequest} from "../../../../services/models/user/get-users.model";
import {User} from "../../../../models/user";

export interface GetRecipientPayload {
  request: GetUsersRequest;
}

export interface GetRecipientSuccessPayload {
  recipient: User
}
