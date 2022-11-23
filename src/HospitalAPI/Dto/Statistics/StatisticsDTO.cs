namespace HospitalAPI.Dto.Statistics
{
    using System.Collections.Generic;

    public class StatisticsDTO
    {
        public IEnumerable<int> Chart1 { get; set; }
        public IEnumerable<string> Chart2Names { get; set; }
        public IEnumerable<int> Chart2Values { get; set; }
        public IEnumerable<int> Chart3Male { get; set; }
        public IEnumerable<int> Chart3Female { get; set; }
        public IEnumerable<int> Chart4 { get; set; }

        public StatisticsDTO()
        {

        }
    }
}
