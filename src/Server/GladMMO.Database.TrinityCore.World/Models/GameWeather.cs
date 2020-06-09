using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class GameWeather
    {
        public uint Zone { get; set; }
        public byte SpringRainChance { get; set; }
        public byte SpringSnowChance { get; set; }
        public byte SpringStormChance { get; set; }
        public byte SummerRainChance { get; set; }
        public byte SummerSnowChance { get; set; }
        public byte SummerStormChance { get; set; }
        public byte FallRainChance { get; set; }
        public byte FallSnowChance { get; set; }
        public byte FallStormChance { get; set; }
        public byte WinterRainChance { get; set; }
        public byte WinterSnowChance { get; set; }
        public byte WinterStormChance { get; set; }
        public string ScriptName { get; set; }
    }
}
