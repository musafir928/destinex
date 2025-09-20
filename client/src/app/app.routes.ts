import { Routes } from '@angular/router';
import { Home } from '../features/home/home';

import { authGuard } from '../core/guards/auth-guard';
import { NotFound } from '../shared/errors/not-found/not-found';
import { ServerError } from '../shared/errors/server-error/server-error';
import { User } from '../features/user/user';
import { adminGuard } from '../core/guards/admin-guard';
import { Admin } from '../features/admin/admin';

export const routes: Routes = [
  { path: '', component: Home },
  { path: 'admin', component: Admin, canActivate: [adminGuard] },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [{ path: 'user', component: User, canActivate: [authGuard] }],
  },
  { path: 'server-error', component: ServerError },
  { path: '**', component: NotFound },
];
