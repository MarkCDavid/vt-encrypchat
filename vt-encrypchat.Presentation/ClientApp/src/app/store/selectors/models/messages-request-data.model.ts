import {User} from "../../../models/user";
import {GetMessagesServiceRequest} from "../../../services/models/message/get-messages.model";
import {SendMessageServiceRequest} from "../../../services/models/message/send-message.model";

export interface MessagesRequestDataModel {
  userId: string | undefined;
  publicKey: string;
  privateKey: string | undefined;
  currentRecipient: User | undefined;
}

export function filledMessagesRequestDataModel(data: MessagesRequestDataModel): boolean {
  if(!data.publicKey || !data.userId || !data.privateKey || !data.currentRecipient) {
    return false;
  }
  return true;
}

export function mapToGetMessagesServiceRequest(data: MessagesRequestDataModel): GetMessagesServiceRequest {
  if (!data.currentRecipient) {
    throw Error();
  }

  return {
    senderId: data.userId,
    recipientId: data.currentRecipient.id,
    senderPrivateGPGKey: data.privateKey,
    senderPublicGPGKey: data.publicKey,
    recipientPublicGPGKey: data.currentRecipient.gpgKey,
  } as GetMessagesServiceRequest;
}

export function mapToSendMessageServiceRequest(data: MessagesRequestDataModel, message: string): SendMessageServiceRequest {
  if (!data.currentRecipient) {
    throw Error();
  }

  return {
    message: message,
    senderId: data.userId,
    recipientId: data.currentRecipient.id,
    senderPrivateGPGKey: data.privateKey,
    senderPublicGPGKey: data.publicKey,
    recipientPublicGPGKey: data.currentRecipient.gpgKey,
  } as SendMessageServiceRequest;
}
