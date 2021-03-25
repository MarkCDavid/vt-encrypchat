export interface PollMessagesRequest {
  senderId: string;
  recipientId: string;
  lastMessageId: string;
}

export interface PollMessagesResponse {
  newMessages: boolean;
}



