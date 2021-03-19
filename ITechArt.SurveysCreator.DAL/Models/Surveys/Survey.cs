using System;
using System.Collections.Generic;

namespace ITechArt.SurveysCreator.DAL.Models.Surveys
{
    public enum Options
    {
        Anonymous = 0b00000001,
        ShowQuestionNumbers = 0b00000010,
        ShowPageNumbers = 0b00000100,
        RandomQuestionOrder = 0b00001000,
        ShowCompletenessProgress = 0b00010000
    }

    public class Survey
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime LastUpdate { get; set; }

        public byte Options { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }

        public ICollection<Page> Pages { get; set; }
    }
}
