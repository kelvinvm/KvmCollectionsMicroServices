import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { AdminLoginComponent } from './components/admin-login/admin-login.component';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { CreateAlbumComponent } from './components/create-album/create-album.component';
import { AdminAlbumsListComponent } from './components/admin-albums-list/admin-albums-list.component';
import { AdminEditAlbumComponent } from './components/admin-edit-album/admin-edit-album.component';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'admin/login',
    component: AdminLoginComponent
  },
  {
    path: 'admin',
    component: AdminDashboardComponent,
    canActivate: [authGuard],
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'albums'
      },
      {
        path: 'albums',
        component: AdminAlbumsListComponent
      },
      {
        path: 'albums/edit/:id',
        component: AdminEditAlbumComponent
      },
      {
        path: 'create-album',
        component: CreateAlbumComponent
      }
    ]
  }
];
