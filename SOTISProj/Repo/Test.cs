using System.Collections.ObjectModel;

namespace SOTISProj.Repo
{
    public class Test
    {
        public int Id { get;private set; }
        public string Name { get;  set; }
        public Field Field { get; set; }
        public ICollection<ConnectQuestion> ConnectQuestions { get; set; }
        public ICollection<DefinitionQuestion> DefinitionQuestions { get; set; }
        public Test() { }
        public bool Validate()
        {
            //TODO: implement model validation
            return true;
        }
    }
}
