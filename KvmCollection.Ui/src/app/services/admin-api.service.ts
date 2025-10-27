import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateAlbumRequest } from '../models/admin.model';
import { Album } from '../models/album.model';

@Injectable({
  providedIn: 'root'
})
export class AdminApiService {
  private http = inject(HttpClient);
  private readonly API_URL = 'https://localhost:7043/api/albums'; // Update with your actual API URL

  createAlbum(albumData: CreateAlbumRequest): Observable<Album> {
    console.log('Creating album with data:', albumData);
    return this.http.post<Album>(this.API_URL, albumData);
  }

  getAlbums(): Observable<Album[]> {
    return this.http.get<Album[]>(this.API_URL);
  }

  getAlbum(id: number): Observable<Album> {
    return this.http.get<Album>(`${this.API_URL}/${id}`);
  }

  updateAlbum(id: number, albumData: CreateAlbumRequest): Observable<Album> {
    return this.http.put<Album>(`${this.API_URL}/${id}`, albumData);
  }

  uploadThumbnail(file: File): Promise<string> {
    return new Promise((resolve, reject) => {
      const formData = new FormData();
      formData.append('thumbnail', file);

      this.http.post<{ url: string }>(`${this.API_URL}/upload-thumbnail`, formData)
        .subscribe({
          next: (response) => resolve(response.url),
          error: (error) => {
            console.error('Error uploading thumbnail:', error);
            reject(error);
          }
        });
    });
  }
}
