import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from "@angular/router";
import {Store} from "@ngrx/store";
import {getUserId, getUserSettingsLoaded} from "../store/selectors";
import {skipWhile, takeWhile, tap, withLatestFrom} from "rxjs/operators";
import {GetUserSettingsRequest} from "../services/models/user/get-user-settings.model";
import {GetUserSettingsPayload} from "../store/actions/payloads/user/get-user-settings.payload";
import {getUserSettings} from "../store/actions";

@Injectable({
  providedIn: 'root'
})
export class UserSettingsGuard implements CanActivate {

  constructor(private store: Store<{}>) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    this.scheduleLoadSettings();
    return true;
  }

  private scheduleLoadSettings() {
    this.store.select(getUserSettingsLoaded).subscribe(userSettingsLoaded => {
      if (userSettingsLoaded) {
        return;
      }
      this.store.select(getUserId).subscribe(userId => {
        if (!userId) {
          return;
        }

        const request = {userId: userId} as GetUserSettingsRequest;
        const payload = {request: request} as GetUserSettingsPayload;
        this.store.dispatch(getUserSettings({payload: payload}));
      });
    })
  }
}
