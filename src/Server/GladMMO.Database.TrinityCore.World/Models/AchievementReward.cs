using System;
using System.Collections.Generic;

namespace GladMMO.Database.TrinityCore.World.Models
{
    public partial class AchievementReward
    {
        public uint Id { get; set; }
        public uint TitleA { get; set; }
        public uint TitleH { get; set; }
        public uint ItemId { get; set; }
        public uint Sender { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public uint? MailTemplateId { get; set; }
    }
}
