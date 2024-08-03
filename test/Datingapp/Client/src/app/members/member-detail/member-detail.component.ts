import { CommonModule } from '@angular/common';
import { ThisReceiver } from '@angular/compiler';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from '../../_models/member';
import { MembersService } from '../../_services/members.service';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryModule, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { TimeagoFormatter, TimeagoModule } from "ngx-timeago";

@Component({
  selector: 'app-member-detail',
  standalone: true,
  imports: [CommonModule,TabsModule,NgxGalleryModule, TimeagoModule,],
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css'
})
export class MemberDetailComponent {
  member: Member ;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  
  constructor(private memberService: MembersService, private route: ActivatedRoute){
    
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
    // this.galleryImages = [
    //   {
    //     small: 'https://randomuser.me/api/portraits/women/54.jpg',
    //     medium: 'https://randomuser.me/api/portraits/women/54.jpg',
    //     big: 'https://randomuser.me/api/portraits/women/54.jpg'
    //   },
    // ]
    this.loadMember();
    
  }

  loadMember(){
     let username = this.route.snapshot.paramMap.get('username');
     console.log(username);
    if(username){
      this.memberService.getMember(username).subscribe(members=>{
        this.member= members;
        this.galleryImages=this.getImages();
        console.log("loaded member", this.member)

      })
    }else{
  
      console.log("loaded member", this.member)
    }
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
}
  // this.route.paramMap.subscribe(params => {
  //   const username = params.get('username');
  //   if (username) {
  //     this.memberService.getMember(username).subscribe(member => {
  //       this.member = member;
  //       console.log("Loaded member:", this.member);
  //     });
  //   }
  // });

