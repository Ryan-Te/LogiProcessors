using PiTung.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LogiProcessers
{

    public class setCodeLoc : Command
    {
        public override string Name => "setCodeLoc";
        public override string Usage => $"{Name} Location(in TUNG folder)";
        public override string Description => "Lets you set the code location to the location at the file listed starting in the TUNG directory";

        public override bool Execute(IEnumerable<string> args)
        {
            if (args.Count() == 0)
                return false;

            LogiProcessers.CodeLocationGlobal = "/" + args.ElementAt(0);

            return true;
        }
    }
}
