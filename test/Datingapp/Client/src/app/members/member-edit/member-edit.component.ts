import { CommonModule } from '@angular/common';
import { Component, HostListener, ViewChild, viewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { Member } from '../../_models/member';
import { User } from '../../_models/user';
import { AccountService } from '../../_services/account.service';
import { MembersService } from '../../_services/members.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [CommonModule,TabsModule, FormsModule],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent {
@ViewChild('editForm') editForm : NgForm ;
  member: Member;
  user: User;

  @HostListener('window:beforeunload',['$event']) unloadNotification($event:any){
    if(this.editForm?.dirty){
      $event.returnValue=true;
    }
  }

  constructor(private accountService: AccountService, private memberService: MembersService,
    private toastr: ToastrService, private route: Router){
    this.accountService.currentUser$.pipe(take(1)).subscribe(user=>this.user=user);
    this.loadMember();
  }
  loadMember(){
      this.memberService.getMember(this.user.username).subscribe(member=>{
        this.member=member;
        console.log(this.member);
      })
  }
  updateMember(){
    console.log("update is clicked");
    this.memberService.updateMember(this.member).subscribe(()=>{
      this.toastr.success('Profile updated Successfully');
      this.editForm.reset(this.member);
      this.route.navigate['/members'];
    })
  }
}
