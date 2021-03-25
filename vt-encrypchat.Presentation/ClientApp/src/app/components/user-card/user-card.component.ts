import {Component, Input, OnInit} from '@angular/core';
import {User} from "../../models/user";
import {Store} from "@ngrx/store";
import {go, goExtras} from "../../store/actions";
import {ROUTES} from "../../shared/constants/routes.const";
import {NavigationExtras, Params} from "@angular/router";
import {PARAMS} from "../../shared/constants/params.const";

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

  constructor(private store: Store<{}>) { }

  ngOnInit() {

  }

  async message() {
    const queryParams = { } as Params;
    queryParams[PARAMS.Recipient] = this.recipient.id;
    const extras = { queryParams: queryParams } as NavigationExtras;
    this.store.dispatch(goExtras({path: ROUTES.Chat, extras }))
  }
}
