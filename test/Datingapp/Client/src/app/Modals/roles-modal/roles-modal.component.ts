import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { User } from '../../_models/user';

@Component({
  selector: 'app-roles-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './roles-modal.component.html',
  styleUrl: './roles-modal.component.css'
})
export class RolesModalComponent {
  @Input() updateSelectedRoles = new EventEmitter();
  user:User;
  roles:any[];

  constructor(public bsModalRef: BsModalRef){

  }

  updateRoles(){
    this.updateSelectedRoles.emit(this.roles);
    this.bsModalRef.hide();
  }
}
