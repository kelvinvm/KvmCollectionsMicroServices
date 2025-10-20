using KvmCollection.Common.Interfaces;
using System.Drawing;

namespace KvmCollection.Common.Dto;

public record ImageClassification(
    List<string> Categories,
    List<TagInfo> Tags,
    Size AnalyzedSize,
    Size OriginalSize,
    byte[] Image,
    string Description,
    int Oid = -1) : IHaveXpoOid;
