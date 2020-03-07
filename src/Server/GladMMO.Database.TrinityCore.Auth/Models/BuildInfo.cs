using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class BuildInfo
    {
        public int Build { get; set; }
        public int? MajorVersion { get; set; }
        public int? MinorVersion { get; set; }
        public int? BugfixVersion { get; set; }
        public string HotfixVersion { get; set; }
        public string WinAuthSeed { get; set; }
        public string Win64AuthSeed { get; set; }
        public string Mac64AuthSeed { get; set; }
        public string WinChecksumSeed { get; set; }
        public string MacChecksumSeed { get; set; }
    }
}
