import {User} from "../../../models/user";
import {Message} from "../../../models/message";

export interface GetMessagesServiceRequest {
  senderId: string;
  recipientId: string;
  senderPrivateGPGKey: string;
  senderPublicGPGKey: string;
  recipientPublicGPGKey: string;
}

export interface GetMessagesRequest {
  sender: string;
  recipient: string;
}

export interface GetMessagesResponse {
  messages: Message[]
}

export interface EncryptedMessage {
  id: string;
  fromValue: string;
  toValue: string;
  dateTime: Date;
  from: User;
  to: User;
}



