import { Injectable } from '@angular/core';
import * as bcrypt from 'bcryptjs';
import * as openpgp from 'openpgp';

@Injectable({
  providedIn: 'root'
})
export class CryptoService {

  constructor() { }

  public hash(value: string): string {
    return bcrypt.hashSync(value, "$2a$10$GY93Pgew4tiVQ4r9hvBpOe");
  }

  public async sign(value: string, privateKey: string): Promise<string> {
    const _privateKey = await openpgp.readKey({armoredKey: privateKey});
    await _privateKey.decrypt("");

    const unsignedMessage = openpgp.CleartextMessage.fromText(value);
    return await openpgp.sign({
      message: unsignedMessage,
      privateKeys: _privateKey
    });
  }

  public async checkSignature(value: string, publicKey: string): Promise<boolean> {

    const _publicKey = await openpgp.readKey({ armoredKey: publicKey });

    const signedMessage = await openpgp.readCleartextMessage({
      cleartextMessage: value
    });

    const verification = await openpgp.verify({
      message: signedMessage,
      publicKeys: _publicKey
    });

    const { valid } = verification.signatures[0];
    return valid;
  }

  public async encrypt(value: string, publicKey: string): Promise<string> {
    const _publicKey = await openpgp.readKey({ armoredKey: publicKey });

    const message = openpgp.Message.fromText(value);

    return await openpgp.encrypt({
      message: message,
      publicKeys: _publicKey,
    });
  }

  public async decrypt(value: string, privateKey: string): Promise<string> {
    const _privateKey = await openpgp.readKey({ armoredKey: privateKey });
    await _privateKey.decrypt("");

    const message = await openpgp.readMessage({
      armoredMessage: value
    });

    const { data: decrypted } = await openpgp.decrypt({
      message,
      privateKeys: _privateKey
    });

    return decrypted;
  }
}
