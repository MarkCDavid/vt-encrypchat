import {ErrorHandler, Injectable} from '@angular/core';
import {MatDialog} from '@angular/material';
import {ErrorModalComponent} from '../components/error-modal/error-modal.component';


@Injectable()
export class GlobalErrorHandler implements ErrorHandler {

  constructor(public dialog: MatDialog) {
  }

  handleError(error: GeneralError) {
    console.error(error);
    this.openDialog(error);
  }

  openDialog(error: GeneralError): void {
    const dialogRef = this.dialog.open(ErrorModalComponent, {
      width: '500px',
      data: error
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }
}
