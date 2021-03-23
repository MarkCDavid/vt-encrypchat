import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {CryptoService} from "./crypto.service";
import {SendMessageRequest, SendMessageServiceRequest} from "./models/message/send-message.model";
import {Observable, throwError} from "rxjs";
import {API} from "../shared/constants/api.const";
import {catchError, map} from "rxjs/operators";
import {handleError} from "../shared/handlers/http-error-handler";
import {
  DecryptedMessage,
  GetMessagesRequest,
  GetMessagesResponse,
  GetMessagesServiceRequest,
  Message
} from "./models/message/get-messages.model";

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(
    private http: HttpClient,
    private crypto: CryptoService
  ) { }


  public async SendMessage(serviceRequest: SendMessageServiceRequest): Promise<Observable<void>> {
    const url = `${API.Prefix}/${API.Messages}`;
    const options = { headers: {'Content-Type': 'application/json'} };

    const signedMessage = await this.crypto.sign(serviceRequest.message, serviceRequest.senderPrivateGPGKey);

    const body = JSON.stringify({
      senderValue: await this.crypto.encrypt(signedMessage, serviceRequest.senderPublicGPGKey),
      recipientValue: await this.crypto.encrypt(signedMessage, serviceRequest.recipientPublicGPGKey),
      sender: serviceRequest.senderId,
      recipient: serviceRequest.recipientId,
    } as SendMessageRequest)

    return this.http.post(url, body, options).pipe(
      map(() => { console.log("TOP_KEK") }),
      catchError(httpError => throwError(handleError(httpError)) )
    );
  }

  public async GetMessages(serviceRequest: GetMessagesServiceRequest): Promise<Observable<Promise<GetMessagesResponse>>> {
    const url = `${API.Prefix}/${API.Messages}/${serviceRequest.senderId}/${serviceRequest.recipientId}`;

    return this.http.get<Message[]>(url, {}).pipe(
      map(async (messages: Message[]) => {

        let decryptedMessages = await Promise.all(messages.map(async message => {
          const isSender = message.from.id === serviceRequest.senderId;
          let decryptedMessage = await this.crypto.decrypt(isSender ? message.fromValue : message.toValue, serviceRequest.senderPrivateGPGKey);
          let valid = await this.crypto.checkSignature(decryptedMessage, isSender ? serviceRequest.senderPublicGPGKey : serviceRequest.recipientPublicGPGKey);
          return {
            id: message.id,
            message: await this.crypto.cleartext(decryptedMessage),
            valid: valid,
            time: message.dateTime,
            from: message.from,
            to: message.to,
          } as DecryptedMessage;
        }));

        return {
          messages: decryptedMessages
        } as GetMessagesResponse;
      }),
      catchError(httpError => throwError(handleError(httpError)) )
    );
  }
}
