import { Injectable } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { ConfirmDialogueComponent } from '../_models/confirm-dialogue/confirm-dialogue.component';

@Injectable({
  providedIn: 'root'
})
export class ConfirmService {

  bsModelRef: BsModalRef
  constructor(private modalService: BsModalService) { }

  confirm(title= 'Confirmation',
   message='Are Your sure?', 
   btnOkText = 'Ok',
    btnCancelText= 'Cancel'):Observable<boolean>{
      const config = {
        initialState: {
          title,
          message,
          btnOkText,
          btnCancelText
        }
      }
      this.bsModelRef= this.modalService.show(ConfirmDialogueComponent,config);

      return new Observable<boolean>(this.getResult());
    }

    private getResult(){
      return (observer)=>{
        const subscription =this.bsModelRef.onHidden.subscribe(()=>{
          observer.next(this.bsModelRef.content.result);
          observer.complete();
        });

        return{
          unsubscribe(){
            subscription.unsubscribe();
          }
        }
      }
    }
}
