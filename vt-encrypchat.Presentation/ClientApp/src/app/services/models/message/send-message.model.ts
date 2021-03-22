export interface SendMessageServiceRequest {
  senderId: string;
  recipientId: string;
  message: string;
  senderPrivateGPGKey: string;
  senderPublicGPGKey: string;
  recipientPublicGPGKey: string;
}

export interface SendMessageRequest {
  senderValue: string;
  recipientValue: string;
  sender: string;
  recipient: string;
}
