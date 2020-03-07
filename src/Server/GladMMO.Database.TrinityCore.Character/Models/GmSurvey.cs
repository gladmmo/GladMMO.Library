using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class GmSurvey
    {
        public uint SurveyId { get; set; }
        public uint Guid { get; set; }
        public uint MainSurvey { get; set; }
        public string Comment { get; set; }
        public uint CreateTime { get; set; }
    }
}
