using System;
using gtdpad.Support;

namespace gtdpad.Domain
{
    public abstract class Section
    {
        protected Section(
            Guid id,
            Guid owner,
            string title,
            int order)
        {
            Guard.AgainstEmpty(id, nameof(id));
            Guard.AgainstEmpty(owner, nameof(owner));
            Guard.AgainstNullOrEmpty(title, nameof(title));

            ID = id;
            Owner = owner;
            Title = title;
            Order = order;
        }

        public Guid ID { get; }

        public Guid Owner { get; }

        public string Title { get; }

        public int Order { get; set; }
    }
}
