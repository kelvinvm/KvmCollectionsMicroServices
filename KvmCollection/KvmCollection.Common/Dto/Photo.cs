namespace KvmCollection.Common.Dto;

public record Photo(int Oid, int AlbumOid, string Url, string ThumbnailUrl, string? Title = null, string? Description = null);

