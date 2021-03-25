import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {GeneralError} from '../../models/general-error';

@Component({
  selector: 'app-error-modal',
  templateUrl: './error-modal.component.html',
  styleUrls: ['./error-modal.component.css']
})
export class ErrorModalComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<ErrorModalComponent>,
               @Inject(MAT_DIALOG_DATA) public data: GeneralError | Error) { }

  ngOnInit() {
  }

  closeClick(): void {
    this.dialogRef.close();
  }


}
