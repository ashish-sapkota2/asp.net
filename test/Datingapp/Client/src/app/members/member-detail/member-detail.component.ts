import { CommonModule } from '@angular/common';
import { ThisReceiver } from '@angular/compiler';
import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from '../../_models/member';
import { MembersService } from '../../_services/members.service';
import { TabDirective, TabsetComponent, TabsModule } from 'ngx-bootstrap/tabs';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryModule, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { TimeagoFormatter, TimeagoModule } from "ngx-timeago";
import { MemberMessagesComponent } from '../member-messages/member-messages.component';
import { Message } from '../../_models/message';
import { MessageService } from '../../_services/message.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-member-detail',
  standalone: true,
  imports: [CommonModule, TabsModule, NgxGalleryModule,
     TimeagoModule, MemberMessagesComponent],
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css'
})
export class MemberDetailComponent{
  @ViewChild('memberTabs', { static: true }) memberTabs: TabsetComponent;
  member: Member;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  activeTab: TabDirective;
  messages: Message[] = [];

  constructor(private memberService: MembersService,
    private route: ActivatedRoute, private messageService: MessageService) {
    this.route.data.subscribe(data => {
      this.member = data['member'];
    })
    setTimeout(()=>{

      this.route.queryParams.subscribe(params => {
        params['tab'] ? this.selectTab(params['tab']) : this.selectTab(0);
      })
    },50)

    this.galleryOptions = [
      {
        height: '500px',
        width: '500px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: true
      }
    ]
    this.galleryImages = this.getImages();
    // this.galleryImages = [
    //   {
      //     small: 'https://randomuser.me/api/portraits/women/54.jpg',
      //     medium: 'https://randomuser.me/api/portraits/women/54.jpg',
      //     big: 'https://randomuser.me/api/portraits/women/54.jpg'
    //   },
    // ]

  }
  
  
  selectTab(tabId: number) {
    this.memberTabs.tabs[tabId].active = true;
  }

 // loadMember(){
  //    let username = this.route.snapshot.paramMap.get('username');
  //   if(username){
  //     this.memberService.getMember(username).subscribe(members=>{
  //       this.member= members;
  //     })
  //   }else{
  //     console.log("loaded member", this.member)
  //   }
  // }

  getImages(): NgxGalleryImage[] {
    const imageUrls = [];
    const photos = this.member?.photos;
    if (photos) {
      for (const photo of photos) {
        imageUrls.push({
          small: photo?.url,
          medium: photo?.url,
          big: photo?.url,
        })
      }
    }
    return imageUrls

  }
  loadMessages() {
    this.messageService.getMessageThread(this.member.username).subscribe(messages => {
      this.messages = messages;
    })
  }

  


  onTabActivated(data: TabDirective) {
    this.activeTab = data;
    if (this.activeTab.heading === 'Messages' && this.messages.length === 0) {
      this.loadMessages()
    }
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

