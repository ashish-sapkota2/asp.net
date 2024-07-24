import { CommonModule } from '@angular/common';
import { Component, HostListener, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { Member } from '../../_models/member';
import { User } from '../../_models/user';
import { AccountService } from '../../_services/account.service';
import { MembersService } from '../../_services/members.service';

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [CommonModule, TabsModule, FormsModule],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent {

  @ViewChild('editForm') editForm: NgForm | undefined;

  member: Member|undefined;
  user: User | null | undefined;
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event:any){
    if(this.editForm?.dirty){
      $event.returnValue= true;
    }
  }

  constructor(private accountService: AccountService, private memberService: MembersService
      ,private toastr: ToastrService
    ){
    this.accountService.currentUser$.pipe(take(1)).subscribe(user=>this.user=user);
    this.loadMember();
  }
  
  loadMember(){
    if(this.user && this.user.username){

      this.memberService.getMember(this.user.username).subscribe(member=>{
        this.member=member;
        console.log(this.member)
      });
    }
  }
  updateMember(){
    console.log(this.member);
    this.toastr.success('Profile updated successfully')
    this.editForm?.reset(this.member);
  }
}
