export interface Album {
  oid: number;
  name: string;
  thumbnailUrl: string;
  photoCount: number;
  subtitle: string;
}

export interface Photo {
  oid: number;
  albumId: string;
  url: string;
  thumbnailUrl: string;
  title?: string;
  description?: string;
}
