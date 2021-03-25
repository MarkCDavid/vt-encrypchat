import {User} from "../../../models/user";
import {GetMessagesServiceRequest} from "../../../services/models/message/get-messages.model";
import {Message} from "../../../models/message";
import {PollMessagesRequest} from "../../../services/models/message/poll-messages.model";

export interface MessagesPollingDataModel {
  userId: string | undefined;
  lastMessage: Message;
  currentRecipient: User | undefined;
}

export function filledMessagesPollingDataModel(data: MessagesPollingDataModel): boolean {
  if(!data.userId || !data.lastMessage || !data.currentRecipient) {
    return false;
  }
  return true;
}

export function mapToPollMessagesRequest(data: MessagesPollingDataModel): PollMessagesRequest {
  if (!data.currentRecipient) {
    throw Error();
  }

  return {
    senderId: data.userId,
    recipientId: data.currentRecipient.id,
    lastMessageId: data.lastMessage.id
  } as PollMessagesRequest;
}
