using System.Collections.ObjectModel;

namespace SOTISProj.Repo
{
    public class Test
    {
        public int Id { get;private set; }
        public Subject Subject { get;  set; }
        public Student Student { get;  set; }
        public Professor Professor { get; set; } 
        public ICollection<ConnectQuestion> ConnectQuestions { get; set; }
        public ICollection<DefinitionQuestion> DefinitionQuestions { get; set; }
        public DateTime DateTime { get; set; }
        public Test() { }
        public bool Validate()
        {
            //TODO: implement model validation
            return true;
        }
    }
}
