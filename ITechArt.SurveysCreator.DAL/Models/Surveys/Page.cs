using System;
using System.Collections.Generic;

namespace ITechArt.SurveysCreator.DAL.Models.Surveys
{
    public class Page
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Index { get; set; }

        public string SurveyId { get; set; }

        public Survey Survey { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
