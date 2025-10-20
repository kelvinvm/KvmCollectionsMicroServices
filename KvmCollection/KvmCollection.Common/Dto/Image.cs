using KvmCollection.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KvmCollection.Common.Dto;

public record Image(byte[] ImageBytes, int Oid = -1) : IHaveXpoOid;

