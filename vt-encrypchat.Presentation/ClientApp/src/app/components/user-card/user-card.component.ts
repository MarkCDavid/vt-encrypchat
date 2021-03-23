import {Component, Input, OnInit} from '@angular/core';
import {User} from "../../models/user";
import {MessageService} from "../../services/message.service";
import {GetMessagesServiceRequest} from "../../services/models/message/get-messages.model";
import {Store} from "@ngrx/store";
import {getPrivateGPGKey, getPublicGPGKey, getUserId} from "../../store/selectors";
import {SendMessageServiceRequest} from "../../services/models/message/send-message.model";

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.css'],

})
export class UserCardComponent implements OnInit {

  @Input() public recipient!: User;
  public hasGPGKey(): boolean {
    if(this.recipient.gpgKey) {
      return this.recipient.gpgKey.length > 0
    }
    return false;
  }

  constructor(private store: Store<{}>, private service: MessageService) { }

  ngOnInit() {

  }

  async get() {
    this.store.select(getUserId).subscribe(async senderUserId => {
      this.store.select(getPrivateGPGKey).subscribe(async senderPrivateKey => {
        this.store.select(getPublicGPGKey).subscribe(async senderPublicKey => {
          const request = {
            recipientId: this.recipient.id,
            recipientPublicGPGKey: this.recipient.gpgKey,
            senderId: senderUserId,
            senderPrivateGPGKey: senderPrivateKey,
            senderPublicGPGKey: senderPublicKey,
          } as GetMessagesServiceRequest;

          (await this.service.GetMessages(request)).subscribe(async response => {
            console.log(await response);
          })
        })
      })
    });


  }

  async post() {
    this.store.select(getUserId).subscribe(async senderUserId => {
      this.store.select(getPrivateGPGKey).subscribe(async senderPrivateKey => {
        this.store.select(getPublicGPGKey).subscribe(async senderPublicKey => {
          const request = {
            message: "Hello there!",
            recipientId: this.recipient.id,
            recipientPublicGPGKey: this.recipient.gpgKey,
            senderId: senderUserId,
            senderPrivateGPGKey: senderPrivateKey,
            senderPublicGPGKey: senderPublicKey,
          } as SendMessageServiceRequest;
          (await this.service.SendMessage(request)).subscribe();
        })
      })
    });
  }
}
