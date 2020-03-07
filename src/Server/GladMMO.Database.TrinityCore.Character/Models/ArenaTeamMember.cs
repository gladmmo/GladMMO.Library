using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class ArenaTeamMember
    {
        public uint ArenaTeamId { get; set; }
        public uint Guid { get; set; }
        public ushort WeekGames { get; set; }
        public ushort WeekWins { get; set; }
        public ushort SeasonGames { get; set; }
        public ushort SeasonWins { get; set; }
        public ushort PersonalRating { get; set; }
    }
}
