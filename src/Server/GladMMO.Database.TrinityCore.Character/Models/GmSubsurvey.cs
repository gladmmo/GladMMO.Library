using System;
using System.Collections.Generic;

namespace GladMMO
{
    public partial class GmSubsurvey
    {
        public uint SurveyId { get; set; }
        public uint QuestionId { get; set; }
        public uint Answer { get; set; }
        public string AnswerComment { get; set; }
    }
}
