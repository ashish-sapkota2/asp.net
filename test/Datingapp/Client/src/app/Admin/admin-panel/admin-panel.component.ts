import { Component } from '@angular/core';
import { TabsetComponent, TabsModule } from 'ngx-bootstrap/tabs';
import { HasRoleDirective } from '../../_directives/has-role.directive';
import { PhotoManagementComponent } from '../photo-management/photo-management.component';
import { UserManagementComponent } from '../user-management/user-management.component';

@Component({
  selector: 'app-admin-panel',
  standalone: true,
  imports: [UserManagementComponent, PhotoManagementComponent,
    TabsModule,HasRoleDirective],
  templateUrl: './admin-panel.component.html',
  styleUrl: './admin-panel.component.css'
})
export class AdminPanelComponent {

}
