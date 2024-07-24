import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Member } from '../../_models/member';
import { AccountService } from '../../_services/account.service';
import { MembersService } from '../../_services/members.service';

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent {
  members: Member[]=[];

  constructor(private memberService: MembersService){
    this.loadMembers();
  }
  loadMembers(){
    this.memberService.getMembers().subscribe(member=>{
      this.members= member;
      console.log(this.members)
    })
  }
}
