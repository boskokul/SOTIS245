using SOTISProj.Repo;

namespace SOTISProj.DTO
{
    public class TestSampleDTO
    {
        public int Id { get; set; }
        public DateTime TakenOn { get; set; }
        public int StudentId { get; set; }
        public int TestId { get; set; }
        public List<DefinitionAnswerDTO> DefinitionAnswers { get; set; }
        public List<ConnectAnswerDTO> ConnectAnswers { get; set; }
        public TestSampleDTO() { DefinitionAnswers = new List<DefinitionAnswerDTO>(); ConnectAnswers = new List<ConnectAnswerDTO>(); }
    }
}
