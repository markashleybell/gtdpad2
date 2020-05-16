using System.Collections.Generic;

namespace gtdpad.Support
{
    public class Settings
    {
        public IDictionary<string, string> ConnectionStrings { get; set; }

        public string ConnectionString => ConnectionStrings["Main"];

        public int PersistentSessionLengthInDays { get; set; }
    }
}
