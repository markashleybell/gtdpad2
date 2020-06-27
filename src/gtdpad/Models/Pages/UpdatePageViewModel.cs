using System;

namespace gtdpad.Models
{
    public class UpdatePageViewModel
    {
        public Guid ID { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public int Order { get; set; }
    }
}
