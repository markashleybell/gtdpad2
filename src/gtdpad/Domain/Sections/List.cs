using System;
using System.Collections.Generic;
using System.Linq;

namespace gtdpad.Domain
{
    public class List : Section
    {
        public List(
            Guid id,
            Guid owner,
            string title)
            : this(
                id,
                owner,
                title,
                default)
        {
        }

        public List(
            Guid id,
            Guid owner,
            string title,
            IEnumerable<ListItem> items)
            : base(
                id,
                owner,
                title) =>
            Items = items ?? Enumerable.Empty<ListItem>();

        public IEnumerable<ListItem> Items { get; }

        public List With(string title = default) =>
            new List(ID, Owner, title ?? Title);
    }
}
