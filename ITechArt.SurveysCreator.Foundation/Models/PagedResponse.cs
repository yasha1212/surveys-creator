using System.Collections.Generic;

namespace ITechArt.SurveysCreator.Foundation.Models
{
    public class PagedResponse<T>
    {
        public IReadOnlyCollection<T> Data { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalPagesCount { get; set; }
    }
}
