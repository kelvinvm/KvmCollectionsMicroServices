using KvmCollection.Common.Interfaces;

namespace KvmCollection.Common.Dto;

public record Album(int Oid, string Name, string ThumbnailUrl, int PhotoCount, string Subtitle) : IHaveXpoOid;

