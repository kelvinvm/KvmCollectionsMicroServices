import { Component, ChangeDetectionStrategy, inject, computed, signal } from '@angular/core';
import { AlbumService } from '../../services/album.service';
import { Photo } from '../../models/album.model';
import { PhotoViewerComponent } from '../photo-viewer/photo-viewer.component';

@Component({
  selector: 'app-gallery',
  imports: [PhotoViewerComponent],
  templateUrl: './gallery.component.html',
  styleUrl: './gallery.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class GalleryComponent {
  private albumService = inject(AlbumService);

  protected readonly currentAlbum = this.albumService.currentAlbum;
  protected readonly photos = computed(() => 
    this.albumService.getPhotosByAlbumId(this.albumService.getCurrentAlbumId())
  );

  protected readonly selectedPhoto = signal<Photo | null>(null);
  protected readonly selectedPhotoIndex = signal<number>(-1);

  protected readonly hasNext = computed(() => {
    const index = this.selectedPhotoIndex();
    const photosLength = this.photos().length;
    return index >= 0 && index < photosLength - 1;
  });

  protected readonly hasPrevious = computed(() => {
    const index = this.selectedPhotoIndex();
    return index > 0;
  });

  protected openPhoto(photo: Photo): void {
    const index = this.photos().findIndex(p => p.oid === photo.oid);
    this.selectedPhotoIndex.set(index);
    this.selectedPhoto.set(photo);
  }

  protected closeViewer(): void {
    this.selectedPhoto.set(null);
    this.selectedPhotoIndex.set(-1);
  }

  protected showNext(): void {
    if (this.hasNext()) {
      const newIndex = this.selectedPhotoIndex() + 1;
      this.selectedPhotoIndex.set(newIndex);
      this.selectedPhoto.set(this.photos()[newIndex]);
    }
  }

  protected showPrevious(): void {
    if (this.hasPrevious()) {
      const newIndex = this.selectedPhotoIndex() - 1;
      this.selectedPhotoIndex.set(newIndex);
      this.selectedPhoto.set(this.photos()[newIndex]);
    }
  }
}
