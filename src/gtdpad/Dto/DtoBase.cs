using System;

namespace gtdpad.Dto
{
    public abstract class DtoBase<T>
    {
        public Guid ID { get; set; }
    }
}
