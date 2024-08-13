import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-confirm-dialogue',
  standalone: true,
  imports: [],
  templateUrl: './confirm-dialogue.component.html',
  styleUrl: './confirm-dialogue.component.css'
})
export class ConfirmDialogueComponent {
title:string;
message:string;
btnOkText:string;
btnCancelText:string;
result:boolean;

constructor(public bsModalRef: BsModalRef){}

confirm(){
  this.result=true;
  this.bsModalRef.hide();

}

decline(){
  this.result=false;
  this.bsModalRef.hide();
}
}
