import {Component, Input, OnInit} from '@angular/core';
import {Message} from "../../models/message";
import {Observable} from "rxjs";
import {User} from "../../models/user";
import {Store} from "@ngrx/store";
import {getCurrentRecipient} from "../../store/selectors";

@Component({
  selector: 'app-chat-message',
  templateUrl: './chat-message.component.html',
  styleUrls: ['./chat-message.component.css']
})
export class ChatMessageComponent implements OnInit {

  @Input() public message!: Message;
  recipientId: string = "";

  constructor(private store: Store<{}>) { }

  ngOnInit() {
    this.store.select(getCurrentRecipient).subscribe(recipient => {
      this.recipientId = recipient ? recipient.id : "";
    });
  }

  public isRecipientMessage() : boolean {
    return this.message.from.id == this.recipientId;
  }

  public isSenderMessage() : boolean {
    return !this.isRecipientMessage();
  }

  public isEncrypted() : boolean {
    return !this.message.decrypted;
  }

}
