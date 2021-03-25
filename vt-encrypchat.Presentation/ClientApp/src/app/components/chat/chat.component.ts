import {Component, HostBinding, OnInit} from '@angular/core';
import {Store} from "@ngrx/store";
import {Message} from "../../models/message";
import {
  getCurrentMessages,
  getFoundUsers,
  getPrivateGPGKey,
  getPublicGPGKey,
  getSent,
  getUserId
} from "../../store/selectors";
import {Observable} from "rxjs";
import {GetMessagesPayload} from "../../store/actions/payloads/messages/get-messages.payload";
import {getMessages, getUsers, go, sendMessage} from "../../store/actions";
import {GetMessagesServiceRequest} from "../../services/models/message/get-messages.model";
import {User} from "../../models/user";
import {ActivatedRoute} from "@angular/router";
import {PARAMS} from "../../shared/constants/params.const";
import {ROUTES} from "../../shared/constants/routes.const";
import {skip, take} from "rxjs/operators";
import {GetUsersPayload} from "../../store/actions/payloads/user/get-users.payload";
import {GetUsersRequest} from "../../services/models/user/get-users.model";
import {FormBuilder, FormControl, FormGroup} from "@angular/forms";
import {SendMessagePayload} from "../../store/actions/payloads/messages/send-message.payload";
import {SendMessageServiceRequest} from "../../services/models/message/send-message.model";
import {MessageService} from "../../services/message.service";
import {PollMessagesRequest} from "../../services/models/message/poll-messages.model";

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  public recipient! : User;
  public messages$! : Observable<Message[]>;

  @HostBinding('class.chat') chat: boolean = true;

  textInputGroup!: FormGroup;

  constructor(
    private store: Store<{}>,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private service: MessageService) {
  }

  ngOnInit() {
    this.textInputGroup = this.formBuilder.group({
      textInput: ['']
    });
    this.messages$ = this.store.select(getCurrentMessages)

    const recipientId = this.route.snapshot.queryParamMap.get(PARAMS.Recipient);
    this.store.select(getFoundUsers).pipe(take(1)).subscribe(users => {
        const recipient = users.find(value => value.id === recipientId);
        if( recipient === undefined ) {
          const request = { search: recipientId } as GetUsersRequest;
          const payload = { request: request } as GetUsersPayload;
          this.store.dispatch(getUsers({ payload: payload }));
          this.store.select(getFoundUsers).pipe(skip(1), take(1)).subscribe(users => {
            const recipient = users.find(value => value.id === recipientId);
            if(recipient === undefined) {
              this.store.dispatch(go({ path: ROUTES.Home }));
              return;
            }
            this.recipient = recipient;
            this.scheduleGetMessages();
          });
          return;
        }
        this.recipient = recipient;
        this.scheduleGetMessages();
    })

    this.store.select(getSent).subscribe(sent => {
      if(sent) {
        this.scheduleGetMessages();
      }
    })

    setInterval(() => {
      this.store.select(getUserId).pipe(take(1)).subscribe(userId => {
        this.messages$.pipe(take(1)).subscribe((messages: Message[]) => {
          const lastMessage = messages.slice(-1).pop();
          const request = {
            senderId: userId,
            recipientId: this.recipient.id,
            lastMessageId: lastMessage ? lastMessage.id : "no_id",
          } as PollMessagesRequest;
          this.service.PollMessages(request).subscribe(response => {
            if(response.newMessages) {
              this.scheduleGetMessages();
            }
          })
        })

      })
    }, 5000);
  }

  private scheduleGetMessages(): void {
    this.store.select(getUserId).pipe(take(1)).subscribe(userId => {
      this.store.select(getPublicGPGKey).pipe(take(1)).subscribe(publicKey => {
        this.store.select(getPrivateGPGKey).pipe(take(1)).subscribe(privateKey => {
          const request = {
            senderId: userId,
            recipientId: this.recipient.id,
            senderPrivateGPGKey: privateKey,
            senderPublicGPGKey: publicKey,
            recipientPublicGPGKey: this.recipient.gpgKey,
          } as GetMessagesServiceRequest;
          const payload = { request: request } as GetMessagesPayload;
          this.store.dispatch(getMessages({ payload: payload }))
        })
      })
    })
  }

  send() {
    const control = this.textInputGroup.get("textInput");
    if(control === undefined || control === null) {
      return;
    }

    this.store.select(getUserId).pipe(take(1)).subscribe(userId => {
      this.store.select(getPublicGPGKey).pipe(take(1)).subscribe(publicKey => {
        this.store.select(getPrivateGPGKey).pipe(take(1)).subscribe(privateKey => {
          const request = {
            message: control.value,
            senderId: userId,
            recipientId: this.recipient.id,
            senderPrivateGPGKey: privateKey,
            senderPublicGPGKey: publicKey,
            recipientPublicGPGKey: this.recipient.gpgKey,
          } as SendMessageServiceRequest;
          const payload = { request: request } as SendMessagePayload;
          this.store.dispatch(sendMessage({ payload: payload }))
          control.setValue("");
        })
      })
    })
  }
}
