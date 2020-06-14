using System;
using System.Collections.Generic;
using System.Linq;

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
            if (id == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Page must have an ID.");
            }

            if (owner == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(owner), "Page must have an owner.");
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Page must have a title.");
            }

            if (string.IsNullOrWhiteSpace(slug))
            {
                throw new ArgumentOutOfRangeException(nameof(slug), "Page must have a slug.");
            }

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
