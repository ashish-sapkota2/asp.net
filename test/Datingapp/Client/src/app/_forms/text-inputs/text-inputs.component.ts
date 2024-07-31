import { CommonModule } from '@angular/common';
import { Component, Input, Self } from '@angular/core';
import { AbstractControl, ControlValueAccessor, FormControl, FormsModule, NgControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-text-inputs',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './text-inputs.component.html',
  styleUrl: './text-inputs.component.css'
})
export class TextInputsComponent implements ControlValueAccessor{

  @Input() label: string;
  @Input() type ='text';

  //By using @Self(), you are telling Angular to look only within the current injector for the NgControl dependency. 
  // This means that Angular won't search parent injectors if the dependency is not found in the current injector.
  constructor(@Self() public ngControl: NgControl){
    if(this.ngControl){

      this.ngControl.valueAccessor=this;
    }
  }

  writeValue(obj: any): void {
    
  }
  registerOnChange(fn: any): void {
   
  }
  registerOnTouched(fn: any): void {
    
  }



}
