import {Component, Input, OnInit} from '@angular/core';
import {Message} from "../../models/message";

@Component({
  selector: 'app-chat-message',
  templateUrl: './chat-message.component.html',
  styleUrls: ['./chat-message.component.css']
})
export class ChatMessageComponent implements OnInit {

  @Input() recipient!: string;
  @Input() public message!: Message;

  constructor() { }

  ngOnInit() {
  }

  public isRecipientMessage() : boolean {
    return this.message.from.id == this.recipient;
  }

  public isSenderMessage() : boolean {
    return !this.isRecipientMessage();
  }

}
