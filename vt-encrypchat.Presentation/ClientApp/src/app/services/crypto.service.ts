import { Injectable } from '@angular/core';
import * as bcrypt from 'bcryptjs';

@Injectable({
  providedIn: 'root'
})
export class CryptoService {

  constructor() { }

  public hash(value: string): string {
    return bcrypt.hashSync(value, "$2a$10$GY93Pgew4tiVQ4r9hvBpOe");
  }
}
