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
            string title)
            : this(
                id,
                owner,
                title,
                default)
        {
        }

        public ImageBlock(
            Guid id,
            Guid owner,
            string title,
            IEnumerable<Image> images)
            : base(
                id,
                owner,
                title) =>
            Images = images ?? Enumerable.Empty<Image>();

        public IEnumerable<Image> Images { get; set; }

        public ImageBlock With(string title = default) =>
            new ImageBlock(ID, Owner, title ?? Title);
    }
}
