using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class Conditions
    {
        public int SourceTypeOrReferenceId { get; set; }
        public uint SourceGroup { get; set; }
        public int SourceEntry { get; set; }
        public int SourceId { get; set; }
        public uint ElseGroup { get; set; }
        public int ConditionTypeOrReference { get; set; }
        public byte ConditionTarget { get; set; }
        public uint ConditionValue1 { get; set; }
        public uint ConditionValue2 { get; set; }
        public uint ConditionValue3 { get; set; }
        public byte NegativeCondition { get; set; }
        public uint ErrorType { get; set; }
        public uint ErrorTextId { get; set; }
        public string ScriptName { get; set; }
        public string Comment { get; set; }
    }
}
