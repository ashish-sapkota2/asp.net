import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, FormsModule, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule,FormsModule, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
@Output() cancelRegister = new EventEmitter();
model: any= {};
registerForm : FormGroup

  constructor(private accountService: AccountService){
    this.initializeForm();
  }

initializeForm(){
  this.registerForm= new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('',[Validators.required,
       Validators.minLength(4), Validators.maxLength(8)]),
    confirmPassword: new FormControl('', [Validators.required, this.matchValues('password')])
  })
}
matchValues(matchTo:string): ValidatorFn{
  //all of our forms control are returned from abstractcontrol
  return (control:AbstractControl)=>{
  //control?.value: The value of the form control that this validator is attached to.
// control?.parent?.controls[matchTo].value: The value of the form control that should be matched (where matchTo is the name or key of the control to compare against).
// null: Indicates that the validation has passed (i.e., the values match).
// { isMatching: true }: An object indicating a validation error (i.e., the values do not match).
    return control?.value===control?.parent?.controls[matchTo].value ? null : {isMatching:true}
  }
}
  register(){
    console.log(this.registerForm.value);
    // this.accountService.register(this.model).subscribe(response=>{
    //   console.log(response);
    //   this.cancel();
    // })
  }
  cancel(){
    console.log("Cancelled");
    this.cancelRegister.emit(false);
  }
}
