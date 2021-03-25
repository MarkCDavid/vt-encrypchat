import {Injectable} from '@angular/core';
import {Store} from "@ngrx/store";
import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from "@angular/router";
import {PARAMS} from "../shared/constants/params.const";
import {getCurrentRecipient} from "../store/selectors";
import {getRecipient} from "../store/actions";
import {GetUsersRequest} from "../services/models/user/get-users.model";
import {GetRecipientPayload} from "../store/actions/payloads/messages/get-recipient.payload";
import {take} from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class ChatGuard implements CanActivate {

  constructor(private store: Store<{}>) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    const recipientId = route.queryParamMap.get(PARAMS.Recipient);
    this.store.select(getCurrentRecipient).pipe(take(1)).subscribe(recipient => {
      if (recipient != null && recipient.id === recipientId) {
        return;
      }

      const request = {search: recipientId} as GetUsersRequest;
      const payload = {request: request} as GetRecipientPayload;
      this.store.dispatch(getRecipient({payload: payload}));
    });

    return true;
  }
}
