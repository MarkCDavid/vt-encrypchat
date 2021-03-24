import {User} from "../../../models/user";

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
  messages: DecryptedMessage[]
}

export interface Message {
  id: string;
  fromValue: string;
  toValue: string;
  dateTime: Date;
  from: User;
  to: User;
}

export interface DecryptedMessage {
  id: string;
  message: string;
  valid: boolean;
  time: Date;
  from: User;
  to: User;
}
