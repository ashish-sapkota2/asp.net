import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DateInputComponent } from '../_forms/date-input/date-input.component';
import { TextInputsComponent } from '../_forms/text-inputs/text-inputs.component';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule,FormsModule, 
    ReactiveFormsModule, TextInputsComponent, DateInputComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
@Output() cancelRegister = new EventEmitter();
registerForm : FormGroup;
maxDate : Date ;
validationErrors: string[]=[];

  constructor(private accountService: AccountService, private fb : FormBuilder,
    private toastr: ToastrService, private router: Router
    ){
    this.initializeForm();
    this.maxDate= new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() -18);
  }

initializeForm(){
  this.registerForm=this.fb.group({
    gender: ['male'],
    username: ['', Validators.required],
    knownAs: ['', Validators.required],
    dateOfBirth: ['', Validators.required],
    city: ['', Validators.required],
    country: ['', Validators.required],
    password: ['',[Validators.required,
       Validators.minLength(4), Validators.maxLength(8)]],
    confirmPassword: ['', [Validators.required, this.matchValues('password')]]
  })
  // this.registerForm= new FormGroup({
  //   username: new FormControl('', Validators.required),
  //   password: new FormControl('',[Validators.required,
  //      Validators.minLength(4), Validators.maxLength(8)]),
  //   confirmPassword: new FormControl('', [Validators.required, this.matchValues('password')])
  // })
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
    var result =this.accountService.register(this.registerForm.value).subscribe(response=>{
      this.router.navigateByUrl('/members');
      this.toastr.success("registered Successfully");
      console.log(response);
      // this.cancel();
    })
    if(!result){
      this.validationErrors = ["something went wrong"]
    }
  }
  cancel(){
    console.log("Cancelled");
    this.cancelRegister.emit(false);
  }
}
