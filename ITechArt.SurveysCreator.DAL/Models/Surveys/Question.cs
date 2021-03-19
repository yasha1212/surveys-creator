using System;

namespace ITechArt.SurveysCreator.DAL.Models.Surveys
{
    public enum QuestionType
    {
        SingleAnswer,
        MultipleAnswers,
        Text,
        File,
        Rating,
        Scale
    }

    public class Question
    {
        public string Id { get; set; }
        
        public string Text { get; set; }

        public int Index { get; set; }

        public bool Required { get; set; }

        public QuestionType Type { get; set; }

        public byte[] Body { get; set; }

        public string PageId { get; set; }

        public Page Page { get; set; }
    }
}
