import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Photo } from '../../_models/photo';
import { AdminService } from '../../_services/admin.service';

@Component({
  selector: 'app-photo-management',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './photo-management.component.html',
  styleUrl: './photo-management.component.css'
})
export class PhotoManagementComponent {
  photos: Photo[];

  constructor(private adminService:AdminService){
    this.getPhotoForApproval();
  }

  getPhotoForApproval(){
    this.adminService.getPhotosForApproval().subscribe(photos=>{
      this.photos=photos;
    })
  }

  approvePhoto(photoId){
    this.adminService.approvePhoto(photoId).subscribe(()=>{
      this.photos.splice(this.photos.findIndex(p=>p.id===photoId),1);
    })
  }

  rejectPhoto(photoId){
    this.adminService.rejectPhoto(photoId).subscribe(()=>{
      this.photos.splice(this.photos.findIndex(p=>p.id===photoId),1)
    })
  }
}
