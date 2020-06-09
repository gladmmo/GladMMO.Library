using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class ItemTemplateLocale
    {
        public uint Id { get; set; }
        public string Locale { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short? VerifiedBuild { get; set; }
    }
}
