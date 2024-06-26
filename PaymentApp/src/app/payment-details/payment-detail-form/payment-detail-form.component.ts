import { Component } from '@angular/core';
import { PaymentDetailService } from '../../shared/payment-detail.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-payment-detail-form',
  templateUrl: './payment-detail-form.component.html',
  styles: ``
})
export class PaymentDetailFormComponent {
  constructor(public service: PaymentDetailService){

  }
  onSubmit(form:NgForm){
    this.service.postPaymentDetail()
    .subscribe({
      next: res =>{
        console.log(res)
      },
      error : err=>{console.log(err)}
    })
  }
}
