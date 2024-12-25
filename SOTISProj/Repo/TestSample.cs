namespace SOTISProj.Repo
{
    public class TestSample
    {
        public int Id { get; set; }
        public DateTime TakenOn { get; set; }
        public Student Student { get; set; }
        public Test Test { get; set; }
        public ICollection<DefinitionAnswer> DefinitionAnswers { get; set; } 
        public ICollection<ConnectAnswer> ConnectAnswers { get; set; }
        public TestSample() { 
            DefinitionAnswers = new List<DefinitionAnswer>();
            ConnectAnswers = new List<ConnectAnswer>();
        }
    }
}
