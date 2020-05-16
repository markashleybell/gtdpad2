using System;

namespace gtdpad.Domain
{
    public class ListItem
    {
        public ListItem(
            Guid id,
            string text)
        {
            ID = id;
            Text = text;
        }

        public Guid ID { get; set; }

        public string Text { get; }
    }
}
