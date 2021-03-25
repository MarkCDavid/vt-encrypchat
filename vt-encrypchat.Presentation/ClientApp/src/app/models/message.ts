import {User} from "./user";

export interface Message {
  id: string;
  message: string;
  decrypted: boolean;
  valid: boolean;
  time: Date;
  from: User;
  to: User;
}
