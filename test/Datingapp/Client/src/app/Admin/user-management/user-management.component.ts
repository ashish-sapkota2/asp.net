import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { User } from '../../_models/user';
import { AdminService } from '../../_services/admin.service';
import { BsModalRef, BsModalService, ModalModule, ModalOptions } from 'ngx-bootstrap/modal';
import { RolesModalComponent } from '../../Modals/roles-modal/roles-modal.component';

@Component({
  selector: 'app-user-management',
  standalone: true,
  imports: [CommonModule, FormsModule,RolesModalComponent],
  templateUrl: './user-management.component.html',
  styleUrl: './user-management.component.css'
})
export class UserManagementComponent {

  users: Partial<User[]>;
  bsModalRef: BsModalRef;

  constructor(private adminService: AdminService, private modalService: BsModalService){
    this.getUsersWithRoles();
  }

  getUsersWithRoles(){
    this.adminService.getUsersWithRoles().subscribe(users=>{
      this.users=users;
    })
  }

  openRolesModal(user:User){
    const config ={
      class: 'model-dialog-centered',
      initialState: {
        user,
        roles: this.getRolesArray(user)
      }
    }
    this.bsModalRef = this.modalService.show(RolesModalComponent, config);
    this.bsModalRef.content.updateSelectedRoles.subscribe(values=>{
      const rolesToUpdate ={
        roles: [...values.filter(el=>el.checked===true).map(el=>el.name)]
      };
      if(rolesToUpdate){
        this.adminService.updateUserRoles(user.username,rolesToUpdate.roles).subscribe(()=>{
          user.roles=[...rolesToUpdate.roles]
        })
      }
    })
  }

  private getRolesArray(user){
    const roles =[];
    const userRoles =user.roles;
    const availableRoles : any[]=[
      {name:'Admin', value:'Admin'},
      {name:'Moderator', value:'Moderator'},
      {name:'Member', value:'Member'}
    ];

    availableRoles.forEach(role=>{
      let isMatch =false;
      for(const userRole of userRoles){
        if(role.name===userRole){
          isMatch=true;
          role.checked=true;
          roles.push(role);
          break;
        }
      }
      if(!isMatch){
        role.checked=false;
        roles.push(role);
      }
    })
    return roles;
  }

}
