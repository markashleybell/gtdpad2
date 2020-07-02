using System;
using System.Collections.Generic;
using gtdpad.Domain;

namespace gtdpad.Models
{
    public class IndexViewModel
    {
        public string User { get; set; }

        public Guid PageID { get; set; }

        public string PageTitle { get; set; }

        public IEnumerable<Page> Pages { get; set; }
    }
}
