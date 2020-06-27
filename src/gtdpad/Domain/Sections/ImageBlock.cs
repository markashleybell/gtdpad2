using System;
using System.Collections.Generic;
using System.Linq;

namespace gtdpad.Domain
{
    public class ImageBlock : Section
    {
        public ImageBlock(
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

        public ImageBlock(
            Guid id,
            Guid owner,
            string title,
            int order,
            IEnumerable<Image> images)
            : base(
                id,
                owner,
                title,
                order) =>
            Images = images ?? Enumerable.Empty<Image>();

        public IEnumerable<Image> Images { get; set; }

        public ImageBlock With(string title = default, int? order = default) =>
            new ImageBlock(ID, Owner, title ?? Title, order ?? Order);
    }
}
