import {GetMessagesResponse} from "../../../../services/models/message/get-messages.model";
import {GetMessagesSuccessPayload} from "../../../actions/payloads/messages/get-messages.payload";

export function mapGetMessagesSuccessPayload(response: GetMessagesResponse): GetMessagesSuccessPayload {
  return {
    messages: response.messages
  };
}
