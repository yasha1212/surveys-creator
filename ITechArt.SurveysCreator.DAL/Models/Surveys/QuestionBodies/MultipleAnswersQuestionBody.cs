using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITechArt.SurveysCreator.DAL.Models.Surveys.QuestionBodies
{
    public class MultipleAnswersQuestionBody
    {
        public IList<string> Answers { get; set; }
    }
}
