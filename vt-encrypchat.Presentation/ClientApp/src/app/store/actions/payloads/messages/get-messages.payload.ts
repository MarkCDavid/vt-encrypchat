import {GetMessagesServiceRequest} from "../../../../services/models/message/get-messages.model";
import {Message} from "../../../../models/message";

export interface GetMessagesPayload {
  request: GetMessagesServiceRequest;
}

export interface GetMessagesSuccessPayload {
  messages: Message[]
}
