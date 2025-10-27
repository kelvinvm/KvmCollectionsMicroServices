import { Injectable, signal } from '@angular/core';
import { Album, Photo } from '../models/album.model';

@Injectable({
  providedIn: 'root'
})
export class AlbumService {
  private albums = signal<Album[]>([
    { oid: 1, name: 'Default Album', photoCount: 6, thumbnailUrl: 'https://picsum.photos/400/300?random=100', subtitle: 'October 2025' },
    { oid: 2, name: 'Vacation 2024', photoCount: 8, thumbnailUrl: 'https://picsum.photos/400/300?random=101', subtitle: 'August 2024' },
    { oid: 3, name: 'Family Photos', photoCount: 12, thumbnailUrl: 'https://picsum.photos/400/300?random=102', subtitle: 'December 2024' },
    { oid: 4, name: 'Nature Collection', photoCount: 10, thumbnailUrl: 'https://picsum.photos/400/300?random=103', subtitle: 'June 2024' },
    { oid: 5, name: 'Urban Landscapes', photoCount: 7, thumbnailUrl: 'https://picsum.photos/400/300?random=104', subtitle: 'March 2025' }
  ]);

  private photos = signal<Photo[]>([
    // Default Album photos
    { oid: 1, albumId: '1', url: 'https://picsum.photos/800/600?random=1', thumbnailUrl: 'https://picsum.photos/300/200?random=1', title: 'Photo 1' },
    { oid: 2, albumId: '1', url: 'https://picsum.photos/800/600?random=2', thumbnailUrl: 'https://picsum.photos/300/200?random=2', title: 'Photo 2' },
    { oid: 3, albumId: '1', url: 'https://picsum.photos/800/600?random=3', thumbnailUrl: 'https://picsum.photos/300/200?random=3', title: 'Photo 3' },
    { oid: 4, albumId: '1', url: 'https://picsum.photos/800/600?random=4', thumbnailUrl: 'https://picsum.photos/300/200?random=4', title: 'Photo 4' },
    { oid: 5, albumId: '1', url: 'https://picsum.photos/800/600?random=5', thumbnailUrl: 'https://picsum.photos/300/200?random=5', title: 'Photo 5' },
    { oid: 6, albumId: '1', url: 'https://picsum.photos/800/600?random=6', thumbnailUrl: 'https://picsum.photos/300/200?random=6', title: 'Photo 6' },
    // Vacation 2024 photos
    { oid: 7, albumId: '2', url: 'https://picsum.photos/800/600?random=7', thumbnailUrl: 'https://picsum.photos/300/200?random=7', title: 'Beach Day' },
    { oid: 8, albumId: '2', url: 'https://picsum.photos/800/600?random=8', thumbnailUrl: 'https://picsum.photos/300/200?random=8', title: 'Mountain View' },
    { oid: 9, albumId: '2', url: 'https://picsum.photos/800/600?random=9', thumbnailUrl: 'https://picsum.photos/300/200?random=9', title: 'Sunset' },
    { oid: 10, albumId: '2', url: 'https://picsum.photos/800/600?random=10', thumbnailUrl: 'https://picsum.photos/300/200?random=10', title: 'City Lights' },
    { oid: 11, albumId: '2', url: 'https://picsum.photos/800/600?random=11', thumbnailUrl: 'https://picsum.photos/300/200?random=11', title: 'Local Food' },
    { oid: 12, albumId: '2', url: 'https://picsum.photos/800/600?random=12', thumbnailUrl: 'https://picsum.photos/300/200?random=12', title: 'Adventure' },
    { oid: 13, albumId: '2', url: 'https://picsum.photos/800/600?random=13', thumbnailUrl: 'https://picsum.photos/300/200?random=13', title: 'Memories' },
    { oid: 14, albumId: '2', url: 'https://picsum.photos/800/600?random=14', thumbnailUrl: 'https://picsum.photos/300/200?random=14', title: 'Journey' }
  ]);

  private currentAlbumId = signal<number>(1);

  readonly allAlbums = this.albums.asReadonly();
  readonly currentAlbum = signal<Album | undefined>(this.albums()[0]);

  getAlbums() {
    return this.albums();
  }

  getPhotosByAlbumId(albumId: number) {
    return this.photos().filter(photo => photo.albumId === albumId.toString());
  }

  getCurrentAlbumPhotos() {
    return this.getPhotosByAlbumId(this.currentAlbumId());
  }

  setCurrentAlbum(albumId: number) {
    this.currentAlbumId.set(albumId);
    const album = this.albums().find(a => a.oid === albumId);
    this.currentAlbum.set(album);
  }

  getCurrentAlbumId() {
    return this.currentAlbumId();
  }
}
