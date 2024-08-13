import { inject } from '@angular/core';
import { CanDeactivateFn } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';
import { ConfirmService } from '../_services/confirm.service';

// Define the guard
export const preventUnsavedChangesGuard: CanDeactivateFn<MemberEditComponent> = (
  component: MemberEditComponent,
  currentRoute,
  currentState,
  nextState
) => {
  const confirmService = inject(ConfirmService);

  // Check if the component is dirty and needs confirmation
  if (component.editForm.dirty) {
    return confirmService.confirm(); // This should return an Observable<boolean>
  }

  return true; // Allow navigation if the form is not dirty
};
