import {GetMessagesResponse} from "../../../../services/models/message/get-messages.model";
import {GetMessagesSuccessPayload} from "../../../actions/payloads/messages/get-messages.payload";
import {GetUsersResponse} from "../../../../services/models/user/get-users.model";
import {GetRecipientSuccessPayload} from "../../../actions/payloads/messages/get-recipient.payload";

export function mapGetRecipientSuccessPayload(response: GetUsersResponse): GetRecipientSuccessPayload {
  return {
    recipient: response.users[0]
  };
}
