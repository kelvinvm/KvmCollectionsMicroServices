export interface CreateAlbumDto {
  name: string;
  subtitle: string;
  thumbnailUrl: string;
}

export interface CreateAlbumRequest {
  name: string;
  subtitle: string;
  thumbnailUrl?: string;
}
