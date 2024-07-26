import { CommonModule } from '@angular/common';
import { ThisReceiver } from '@angular/compiler';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from '../../_models/member';
import { MembersService } from '../../_services/members.service';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryModule, NgxGalleryOptions } from '@kolkov/ngx-gallery';

@Component({
  selector: 'app-member-detail',
  standalone: true,
  imports: [CommonModule,TabsModule, NgxGalleryModule],
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css'
})
export class MemberDetailComponent {
member: Member;
galleryOptions: NgxGalleryOptions[]=[];
galleryImages: NgxGalleryImage[]=[];

constructor(private memberService: MembersService, private route: ActivatedRoute){
  this.loadMember();

  this.galleryOptions =[
    {
      height: '500px',
      width: '500px',
      imagePercent : 100,
      thumbnailsColumns: 4,
      imageAnimation: NgxGalleryAnimation.Slide,
      preview:true
    }
  ]

}
getImages():NgxGalleryImage[]{
  const imageUrls =[];
  const photos = this.member?.photos;
  if(photos){
    for(const photo of photos){
      imageUrls.push({
        small:photo?.url,
        medium:photo?.url,
        big: photo?.url,
      })
    }
  }
  return imageUrls
}
loadMember(){
  let username = this.route.snapshot.paramMap.get('username');
  if(username){
    console.log(username);
    this.memberService.getMember(username).subscribe(member=>{
      this.member= member;
      this.galleryImages =this.getImages();
      console.log("loaded member", this.member)
    })
  }
}
}
