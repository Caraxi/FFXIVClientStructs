﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace FFXIVClientStructs
{
    public static partial class Resolver
    {
        public static bool Initialized = false;

        public static void Initialize()
        {
            if (Initialized) return;

            var module = Process.GetCurrentProcess().MainModule;
            var scanner = new SigScanner(module);

            InitializeMemberFunctions(scanner);

            Initialized = true;
        }
    }
}
