import { Component, Input } from '@angular/core';
import { Member } from '../../_models/member';

@Component({
  selector: 'app-member-card',
  standalone: true,
  imports: [],
  templateUrl: './member-card.component.html',
<<<<<<< HEAD
  styleUrl: './member-card.component.css'
})
export class MemberCardComponent {

  @Input() members : Member | undefined;

  getmembers(){
    console.log(this.members)
  }
=======
  styleUrl: './member-card.component.css',
})
export class MemberCardComponent {
@Input() member: Member | undefined;
>>>>>>> 8ea7a4d8410c2c500a50f421eb5fc5472786f690
}
