import {Component, Input, OnInit} from '@angular/core';
import {User} from "../../models/user";

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.css'],

})
export class UserCardComponent implements OnInit {

  @Input() public user!: User;
  public hasGPGKey(): boolean {
    if(this.user.gpgKey) {
      return this.user.gpgKey.length > 0
    }
    return false;
  }

  constructor() { }

  ngOnInit() {

  }

}
