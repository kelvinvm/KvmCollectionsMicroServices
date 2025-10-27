using KvmCollection.Common.Interfaces;
using System;
using System.Linq;

namespace KvmCollection.Common.Dto;

public record WebImage(string Name, string Url, int Oid = -1) : IHaveXpoOid;

