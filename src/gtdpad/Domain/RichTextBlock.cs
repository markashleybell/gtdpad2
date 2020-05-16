using System;

namespace gtdpad.Domain
{
    public class RichTextBlock : Section
    {
        public RichTextBlock(
            Guid id,
            string title,
            string text)
        : base(
              id,
              title) =>
            Text = text;

        public string Text { get; }
    }
}
