﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codex.Logging
{
    public class ConsoleLogger : TextLogger
    {
        public ConsoleLogger() : base(Console.Out)
        {
        }

        public override void LogError(string error)
        {
            Console.Error.WriteLine(error);
        }

        public override void LogMessage(string message, MessageKind kind)
        {
            if (kind == MessageKind.Informational)
            {
                base.LogMessage(message, kind);
            }
        }
    }
}
