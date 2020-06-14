using System;
using System.Collections.Generic;
using System.Linq;

namespace gtdpad.Domain
{
    public class Page
    {
        public Page(
            Guid id,
            string title,
            string slug)
            : this(
                  id,
                  title,
                  slug,
                  default)
        {
        }

        public Page(
            Guid id,
            string title,
            string slug,
            IEnumerable<Section> sections)
        {
            ID = id;
            Title = title;
            Slug = slug;
            Sections = sections ?? Enumerable.Empty<Section>();
        }

        public Guid ID { get; }

        public string Title { get; }

        public string Slug { get; }

        public IEnumerable<Section> Sections { get; set; }
    }
}
