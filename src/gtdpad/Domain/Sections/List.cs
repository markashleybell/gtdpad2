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
            string title,
            int order)
            : this(
                id,
                owner,
                title,
                order,
                default)
        {
        }

        public List(
            Guid id,
            Guid owner,
            string title,
            int order,
            IEnumerable<ListItem> items)
            : base(
                id,
                owner,
                title,
                order) =>
            Items = items ?? Enumerable.Empty<ListItem>();

        public IEnumerable<ListItem> Items { get; }

        public List With(string title = default, int? order = default) =>
            new List(ID, Owner, title ?? Title, order ?? Order);
    }
}
