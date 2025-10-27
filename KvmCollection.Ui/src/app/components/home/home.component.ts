import { Component, ChangeDetectionStrategy } from '@angular/core';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { GalleryComponent } from '../gallery/gallery.component';
import { LogoComponent } from '../logo/logo.component';

@Component({
  selector: 'app-home',
  imports: [SidebarComponent, GalleryComponent, LogoComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomeComponent {
}
