import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Member } from '../../_models/member';
import { MembersService } from '../../_services/members.service';
<<<<<<< HEAD
=======
import { CommonModule } from '@angular/common';
>>>>>>> 8ea7a4d8410c2c500a50f421eb5fc5472786f690
import { MemberCardComponent } from '../member-card/member-card.component';

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [CommonModule, MemberCardComponent],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent {
<<<<<<< HEAD
members: Member[]=[];

constructor(private memberService: MembersService){
  this.loadMembers();
=======
 members: Member[]=[];

 constructor(private memberService: MembersService){
  this.loadMembers();
 }
 loadMembers(){
  this.memberService.getMembers().subscribe(member=>{
    this.members= member;
  })
 }
>>>>>>> 8ea7a4d8410c2c500a50f421eb5fc5472786f690
}

loadMembers(){
  this.memberService.getMembers().subscribe(members=>{
    this.members= members
  })
}
}


