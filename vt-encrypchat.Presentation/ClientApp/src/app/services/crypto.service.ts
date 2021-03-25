import {Injectable} from '@angular/core';
import * as bcrypt from 'bcryptjs';
import * as openpgp from 'openpgp';
import {Store} from "@ngrx/store";
import {MessageDecryption} from "./models/message/message-decryption.model";

@Injectable({
  providedIn: 'root'
})
export class CryptoService {

  constructor(private store: Store<{}>) {
  }

  public hash(value: string): string {
    return bcrypt.hashSync(value, "$2a$10$GY93Pgew4tiVQ4r9hvBpOe");
  }

  public async sign(value: string, privateKey: string): Promise<string> {
    try {
      const _privateKey = await openpgp.readKey({armoredKey: privateKey});

      const unsignedMessage = openpgp.CleartextMessage.fromText(value);
      return await openpgp.sign({
        message: unsignedMessage,
        privateKeys: _privateKey
      });
    } catch {
      return value;
    }
  }

  public async checkSignature(value: string, publicKey: string): Promise<boolean> {
    try {
      const _publicKey = await openpgp.readKey({armoredKey: publicKey});

      const signedMessage = await openpgp.readCleartextMessage({
        cleartextMessage: value
      });

      const verification = await openpgp.verify({
        message: signedMessage,
        publicKeys: _publicKey,
        streaming: false
      });

      const {verified} = verification.signatures[0];
      return await verified || false;
    } catch {
      return false;
    }
  }

  public async encrypt(value: string, publicKey: string): Promise<string> {
    try {
      const _publicKey = await openpgp.readKey({armoredKey: publicKey});
      const message = openpgp.Message.fromText(value);
      return await openpgp.encrypt({
        message: message,
        publicKeys: _publicKey,
      });
    } catch {
      return value;
    }
  }

  public async decrypt(value: string, privateKey: string): Promise<MessageDecryption> {
    try {
      const _privateKey = await openpgp.readKey({armoredKey: privateKey});

      const message = await openpgp.readMessage({
        armoredMessage: value
      });

      const {data: decrypted} = await openpgp.decrypt({
        message,
        privateKeys: _privateKey
      });

      return {message: decrypted, decrypted: true} as MessageDecryption;
    } catch {
      return {message: value, decrypted: false} as MessageDecryption;
    }
  }

  public async cleartext(value: string): Promise<string> {
    try {
      const message = await openpgp.readCleartextMessage({cleartextMessage: value});
      return message.getText();
    } catch {
      return value;
    }
  }
}
