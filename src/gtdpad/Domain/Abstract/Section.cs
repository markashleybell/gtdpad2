using System;

namespace gtdpad.Domain
{
    public abstract class Section
    {
        protected Section(
            Guid id,
            string title)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Section must have an ID,");
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentOutOfRangeException(nameof(title), "Section must have a Title.");
            }

            ID = id;
            Title = title;
        }

        public Guid ID { get; }

        public string Title { get; }
    }
}
