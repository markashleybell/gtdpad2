using System.Collections.Generic;

namespace gtdpad.Support
{
    public class Settings
    {
#pragma warning disable CA2227 // Collection properties should be read only
        public IDictionary<string, string> ConnectionStrings { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        public string ConnectionString => ConnectionStrings["Main"];

        public int PersistentSessionLengthInDays { get; set; }
    }
}
