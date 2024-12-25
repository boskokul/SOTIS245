using SOTISProj.Repo;

namespace SOTISProj.DTO
{
    public class ConnectAnswerDTO
    {
        public int Id { get; set; }
        public int ConnectQuestionId { get; set; }
        public List<Pair> ConnectedPairs { get; set; }
        public int TestSampleId { get; set; }
    }
}
