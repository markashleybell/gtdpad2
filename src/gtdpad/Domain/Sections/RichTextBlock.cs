using System;

namespace gtdpad.Domain
{
    public class RichTextBlock : Section
    {
        public RichTextBlock(
            Guid id,
            Guid owner,
            string title,
            string text)
        : base(
              id,
              owner,
              title) =>
            Text = text;

        public string Text { get; }

        public RichTextBlock With(string title = default, string text = default) =>
            new RichTextBlock(ID, Owner, title ?? Title, text ?? Text);
    }
}
