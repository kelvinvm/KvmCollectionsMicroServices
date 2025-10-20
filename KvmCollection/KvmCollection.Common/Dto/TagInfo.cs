using KvmCollection.Common.Interfaces;

namespace KvmCollection.Common.Dto;

public record TagInfo(string Name, double Confidence, int Oid = -1) : IHaveXpoOid;
