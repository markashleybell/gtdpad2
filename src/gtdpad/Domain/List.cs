using System;
using System.Collections.Generic;
using System.Linq;

namespace gtdpad.Domain
{
    public class List : Section
    {
        public List(
            Guid id,
            string title,
            IEnumerable<ListItem> items = null)
        : base(
              id,
              title) =>
            Items = items ?? Enumerable.Empty<ListItem>();

        public IEnumerable<ListItem> Items { get; }
    }
}
