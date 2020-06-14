using System;

namespace gtdpad.Domain
{
    public abstract class Section
    {
        protected Section(
            Guid id,
            Guid owner,
            string title)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Section must have an ID.");
            }

            if (owner == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(owner), "Section must have an owner.");
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentOutOfRangeException(nameof(title), "Section must have a Title.");
            }

            ID = id;
            Owner = owner;
            Title = title;
        }

        public Guid ID { get; }

        public Guid Owner { get; }

        public string Title { get; }
    }
}
