import { HttpClient, provideHttpClient, withFetch } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { Contacts } from '../Models/contact.model';
import { AsyncPipe } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, AsyncPipe, FormsModule, ReactiveFormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
http= inject(HttpClient);
contactsForm = new FormGroup({
  name: new FormControl<string>(''),
  email: new FormControl<string | null>(''),
  phone: new FormControl<string>(''),
  favourite: new FormControl<boolean>(false)
})
contacts$ = this.getContacts();

onDelete(id:string){
  this.http.delete(`https://localhost:7041/api/Contacts/${id}`)
  .subscribe({
    next : (value)=>{
      alert("Item Deleted");
      this.contacts$= this.getContacts();
    }
  });
}
onFormSubmit(){
  const addContactrequest= {
    name :this.contactsForm.value.name,
    email : this.contactsForm.value.email,
    phone : this.contactsForm.value.phone,
    favourite : this.contactsForm.value.favourite
  }





  this.http.post('https://localhost:7041/api/Contacts', addContactrequest)
  .subscribe({
    next: (value)=> {
      console.log(value);
      this.contacts$ = this.getContacts();
      this.contactsForm.reset();
    }
  });
}
private getContacts():Observable<Contacts[]>{
  return this.http.get<Contacts[]>('https://localhost:7041/api/Contacts');
}
}

