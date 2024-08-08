import { Directive, Input, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { take } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Directive({
  selector: '[appHasRole]', //*appHasRole =["Admin"]
  standalone: true
})
export class HasRoleDirective {
  @Input() appHasRole: string[]=[];
  user: User;

  constructor(private viewContainerRef: ViewContainerRef, private templateRef: TemplateRef<any>,
    private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
      console.log(this.user.roles)
      this.updateView();
    })
    
  }
  private updateView():void{
    if (!this.user?.roles || this.user == null) {
      this.viewContainerRef.clear();
      console.log("here now")
      return;
    }
    setTimeout(()=>{

      const hasRole = this.user.roles.some(role=>this.appHasRole.includes(role));
      console.log(this.appHasRole);
      console.log(hasRole);
      
        if (this.user?.roles.some(r => this.appHasRole.includes(r))) {
          this.viewContainerRef.createEmbeddedView(this.templateRef);
        } else {
          console.log("else case")
          this.viewContainerRef.clear();
        }
      
    },50)
  }
}
