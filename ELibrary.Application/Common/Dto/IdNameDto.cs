using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Application.Common.Dto
{
    public class IdNameDto<TId, TName> where TId : IEquatable<TId> where TName : IEquatable<TName>
    {
        TId Id { get; set; }
        TName Name { get; set; }
    }
    public class IdNameDto<TId> : IdNameDto<TId, string> where TId : IEquatable<TId>
    {
    }

    public class IdNameDto : IdNameDto<long>
    {
    }
}
