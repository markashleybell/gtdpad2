using System;
using System.Collections.Generic;
using System.Linq;
using gtdpad.Support;

namespace gtdpad.Domain
{
    public class Page
    {
        public Page(
            Guid id,
            Guid owner,
            string title,
            string slug)
            : this(
                  id,
                  owner,
                  title,
                  slug,
                  default)
        {
        }

        public Page(
            Guid id,
            Guid owner,
            string title,
            string slug,
            IEnumerable<Section> sections)
        {
            Guard.AgainstEmpty(id, nameof(id));
            Guard.AgainstEmpty(owner, nameof(owner));
            Guard.AgainstNullOrEmpty(title, nameof(title));
            Guard.AgainstNullOrEmpty(slug, nameof(slug));

            ID = id;
            Owner = owner;
            Title = title;
            Slug = slug;
            Sections = sections ?? Enumerable.Empty<Section>();
        }

        public Guid ID { get; }

        public Guid Owner { get; set; }

        public string Title { get; }

        public string Slug { get; }

        public IEnumerable<Section> Sections { get; set; }

        public Page With(string title = default, string slug = default) =>
            new Page(ID, Owner, title ?? Title, slug ?? Slug);
    }
}
