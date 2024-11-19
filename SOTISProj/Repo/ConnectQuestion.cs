using System.Collections.ObjectModel;

namespace SOTISProj.Repo
{
    public class ConnectQuestion
    {
        public int Id { get;private set; }
        public ICollection<String> LeftTerms { get; set; }
        public ICollection<String> RightTerms { get; set; } = new List<String>();
        public ConnectQuestion() { }
        public bool Validate()
        {
            //TODO: implement model validation
            return true;
        }
    }
}
