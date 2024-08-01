import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Member } from '../../_models/member';
import { Pagination } from '../../_models/pagination';
import { AccountService } from '../../_services/account.service';
import { MembersService } from '../../_services/members.service';
import { PaginationModule } from 'ngx-bootstrap/pagination';

import { MemberCardComponent } from '../member-card/member-card.component';
import { FormsModule } from '@angular/forms';
import { UserParams } from '../../_models/userParams';
import { take } from 'rxjs';
import { User } from '../../_models/user';

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [CommonModule, MemberCardComponent, PaginationModule, FormsModule],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent {
  members:Member[];
  pagination:Pagination;
 userparams: UserParams;
 user: User;
 genderList =[{value:'male', display: 'Males'}, {value:'female', display :'Females'}];

  constructor(private memberService: MembersService, private accountService: AccountService){
    this.accountService.currentUser$.pipe(take(1)).subscribe(user=>{
      this.user=user;
      this.userparams= new UserParams(user);
    })
    this.loadMembers();

  }
  loadMembers(){
    this.memberService.getMembers(this.userparams).subscribe(response=>{
      this.members=response.result;
      this.pagination= response.pagination
    })
  }

  resetFilters(){
    this.userparams= new UserParams(this.user);
  }
  pageChanged(event:any){ 
    this.userparams.pageNumber=event.page;
    this.loadMembers();
}

}
