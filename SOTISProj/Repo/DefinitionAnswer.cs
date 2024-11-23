using System.Collections.ObjectModel;

namespace SOTISProj.Repo
{
    public class DefinitionAnswer
    {
        public int Id { get; set; }
        public Test Test { get; set; }
        public DefinitionQuestion Question { get; set; }
        public string AnsweredDefinition { get; set; }
        public bool IsCorrect { get; set; }
        public TestSample TestSample { get; set; }
        public DefinitionAnswer() { }
        public bool Validate()
        {
            //TODO: implement model validation
            return true;
        }
    }
}
