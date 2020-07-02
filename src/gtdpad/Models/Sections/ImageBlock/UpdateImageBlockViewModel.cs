using System;

namespace gtdpad.Models
{
    public class UpdateImageBlockViewModel : SectionViewModelBase
    {
        public Guid ID { get; set; }

        public string Title { get; set; }

        public int Order { get; set; }
    }
}
