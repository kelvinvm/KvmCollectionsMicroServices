import { Component, ChangeDetectionStrategy, inject, signal } from '@angular/core';
import { AlbumService } from '../../services/album.service';

@Component({
  selector: 'app-sidebar',
  imports: [],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SidebarComponent {
  private albumService = inject(AlbumService);

  protected readonly albums = this.albumService.allAlbums;
  protected readonly currentAlbum = this.albumService.currentAlbum;
  protected readonly isMenuOpen = signal(false);

  protected selectAlbum(albumId: number): void {
    this.albumService.setCurrentAlbum(albumId);
    this.isMenuOpen.set(false); // Close menu on mobile after selection
  }

  protected toggleMenu(): void {
    this.isMenuOpen.update(open => !open);
  }

  protected closeMenu(): void {
    this.isMenuOpen.set(false);
  }
}
