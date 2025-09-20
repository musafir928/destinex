import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../services/account-service';
import { ToastService } from '../services/toast-service';

export const adminGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toast = inject(ToastService);

  if (accountService.currentUser()?.role === 'Admin') return true;
  else {
    toast.error("You don't have access to this page");
    return false;
  }
};
