using System;

namespace KvmCollection.Common.Dto;

public record BaseDto(DateTime Inserted, DateTime Updated);

public record CoverArtDto(string Name, byte[] Image, DateTime Inserted, DateTime Updated) 
    : BaseDto(Inserted, Updated);