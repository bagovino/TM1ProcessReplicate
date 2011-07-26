using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessReplicate
{
    public class ProcessParameter
    {
        public string Name { get; private set; }
        public string Prompt { get; private set; }
        public string Type { get; private set; }
        public string Value { get; set; }

        public ProcessParameter(string n, string p, string t, string v)
        {
            this.Name = n;
            this.Prompt = p;
            this.Type = t;
            this.Value = v;
        }
    }
}
