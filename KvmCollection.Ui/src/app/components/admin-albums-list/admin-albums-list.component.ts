import { Component, OnInit, signal, inject, ChangeDetectionStrategy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { AdminApiService } from '../../services/admin-api.service';
import { Album } from '../../models/album.model';

@Component({
  selector: 'app-admin-albums-list',
  imports: [CommonModule, RouterLink],
  templateUrl: './admin-albums-list.component.html',
  styleUrl: './admin-albums-list.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AdminAlbumsListComponent implements OnInit {
  private adminApiService = inject(AdminApiService);
  private readonly API_BASE_URL = 'https://localhost:7043';
  
  albums = signal<Album[]>([]);
  isLoading = signal(true);
  errorMessage = signal('');

  ngOnInit() {
    this.loadAlbums();
  }

  getThumbnailUrl(thumbnailPath: string): string {
    // If the path already includes the protocol, return as-is
    if (thumbnailPath.startsWith('http://') || thumbnailPath.startsWith('https://')) {
      return thumbnailPath;
    }
    // Otherwise, prepend the API base URL
    return `${this.API_BASE_URL}${thumbnailPath}`;
  }

  loadAlbums() {
    this.isLoading.set(true);
    this.errorMessage.set('');

    this.adminApiService.getAlbums().subscribe({
      next: (albums) => {
        this.albums.set(albums);
        this.isLoading.set(false);
      },
      error: (error) => {
        console.error('Error loading albums:', error);
        let errorMsg = 'Failed to load albums. ';
        
        if (error.status === 0) {
          errorMsg += 'Cannot connect to server.';
        } else if (error.error?.message) {
          errorMsg += error.error.message;
        } else {
          errorMsg += `Server error (${error.status})`;
        }
        
        this.errorMessage.set(errorMsg);
        this.isLoading.set(false);
      }
    });
  }
}
