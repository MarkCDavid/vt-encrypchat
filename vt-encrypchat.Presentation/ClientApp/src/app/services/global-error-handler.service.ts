import {ErrorHandler, Injectable} from '@angular/core';
import {MatDialog} from '@angular/material';
import {ErrorModalComponent} from '../components/error-modal/error-modal.component';


@Injectable()
export class GlobalErrorHandler implements ErrorHandler {

  constructor(public dialog: MatDialog) {
  }

  handleError(error: Error) {
    this.openDialog(error);
  }

  openDialog(error: Error): void {
    const dialogRef = this.dialog.open(ErrorModalComponent, {
      width: '500px',
      data: error
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }
}
