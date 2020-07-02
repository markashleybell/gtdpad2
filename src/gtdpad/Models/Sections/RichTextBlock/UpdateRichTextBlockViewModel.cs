using System;

namespace gtdpad.Models
{
    public class UpdateRichTextBlockViewModel : SectionViewModelBase
    {
        public Guid ID { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int Order { get; set; }
    }
}
