using System;
using System.Collections.Generic;
using System.Linq;

namespace gtdpad.Domain
{
    public class ImageBlock : Section
    {
        public ImageBlock(
            Guid id,
            string title,
            IEnumerable<Image> images = null)
        : base(
              id,
              title) =>
            Images = images ?? Enumerable.Empty<Image>();

        public IEnumerable<Image> Images { get; set; }
    }
}
