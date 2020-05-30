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
            string url)
            : this(
                  id,
                  title,
                  url,
                  default)
        {
        }

        public Page(
            Guid id,
            string title,
            string url,
            IEnumerable<Section> sections)
        {
            ID = id;
            Title = title;
            Url = url;
            Sections = sections ?? Enumerable.Empty<Section>();
        }

        public Guid ID { get; }

        public string Title { get; }

        public string Url { get; }

        public IEnumerable<Section> Sections { get; set; }
    }
}
