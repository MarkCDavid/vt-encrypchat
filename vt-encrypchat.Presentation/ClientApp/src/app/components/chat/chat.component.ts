import {Component, HostBinding, OnDestroy, OnInit} from '@angular/core';
import {Store} from "@ngrx/store";
import {Message} from "../../models/message";
import {
  getCurrentMessages,
  getCurrentRecipient,
  getMessagesPollingData,
  getMessagesRequestData,
  getSent
} from "../../store/selectors";
import {Observable} from "rxjs";
import {GetMessagesPayload} from "../../store/actions/payloads/messages/get-messages.payload";
import {getMessages, sendMessage} from "../../store/actions";
import {User} from "../../models/user";
import {skipWhile, take} from "rxjs/operators";
import {FormBuilder, FormGroup} from "@angular/forms";
import {SendMessagePayload} from "../../store/actions/payloads/messages/send-message.payload";
import {MessageService} from "../../services/message.service";
import {
  filledMessagesRequestDataModel,
  mapToGetMessagesServiceRequest,
  mapToSendMessageServiceRequest,
} from "../../store/selectors/models/messages-request-data.model";
import {
  filledMessagesPollingDataModel,
  mapToPollMessagesRequest
} from "../../store/selectors/models/messages-polling-data.model";
import {ActivatedRoute} from "@angular/router";
import {PARAMS} from "../../shared/constants/params.const";

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, OnDestroy {

  public recipient$!: Observable<User | undefined>;
  public messages$!: Observable<Message[]>;

  @HostBinding('class.chat') chat: boolean = true;

  textInputGroup!: FormGroup;
  messagesPolling!: NodeJS.Timeout;

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
    this.recipient$ = this.store.select(getCurrentRecipient);

    this.store.select(getSent).subscribe(sent => {
      if (sent) {
        this.scheduleGetMessages();
      }
    })

    const recipientId = this.route.snapshot.queryParamMap.get(PARAMS.Recipient);
    this.recipient$
      .pipe(skipWhile(recipient => recipient == null || recipient.id !== recipientId), take(1))
      .subscribe(() => {
        this.messagesPolling = setInterval(() => {
          this.store.select(getMessagesPollingData)
            .pipe(skipWhile(data => !filledMessagesPollingDataModel(data)), take(1))
            .subscribe(data => {
              this.service.PollMessages(mapToPollMessagesRequest(data)).subscribe(response => {
                if (response.newMessages) {
                  this.scheduleGetMessages();
                }
              })
            })
        }, 1000);
        this.scheduleGetMessages();
      });
  }

  ngOnDestroy(): void {
    clearTimeout(this.messagesPolling);
  }

  send() {
    const control = this.textInputGroup.get("textInput");
    if (control === undefined || control === null) {
      return;
    }

    this.store.select(getMessagesRequestData)
      .pipe(skipWhile(data => !filledMessagesRequestDataModel(data)), take(1))
      .subscribe(data => {
        const payload = {request: mapToSendMessageServiceRequest(data, control.value)} as SendMessagePayload;
        this.store.dispatch(sendMessage({payload: payload}))
        control.setValue("");
      })
  }

  private scheduleGetMessages(): void {
    this.store.select(getMessagesRequestData)
      .pipe(skipWhile(data => !filledMessagesRequestDataModel(data)), take(1)).subscribe(data => {
      const payload = {request: mapToGetMessagesServiceRequest(data)} as GetMessagesPayload;
      this.store.dispatch(getMessages({payload: payload}))
    })
  }
}
