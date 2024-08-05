import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { MemberCardComponent } from '../members/member-card/member-card.component';
import { Member } from '../_models/member';
import { Pagination } from '../_models/pagination';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-lists',
  standalone: true,
  imports: [CommonModule, FormsModule, MemberCardComponent,PaginationModule],
  templateUrl: './lists.component.html',
  styleUrl: './lists.component.css'
})
export class ListsComponent {

  members : Partial<Member[]>;
  predicate ='liked';
  pageNumber= 1;
  pageSize = 5;
  pagination: Pagination;
  constructor(private memberService: MembersService){
    this.loadLikes();
  }

  loadLikes(){
    this.memberService.getLikes(this.predicate, this.pageNumber, this.pageSize).subscribe(response=>{
      this.members = response.result;
      this.pagination= response.pagination;

    })
  }

  pageChange(event:any){
    this.pageNumber= event.page;
    this.loadLikes();
  }
}
