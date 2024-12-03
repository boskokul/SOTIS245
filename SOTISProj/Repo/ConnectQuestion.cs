using System.Collections.ObjectModel;

namespace SOTISProj.Repo
{
    public class ConnectQuestion
    {
        public int Id { get;private set; }
        public ICollection<String> BelongTerms { get; set; } //terms
        public ICollection<String> BellongingTerms { get; set; } = new List<String>(); //parents
        public ConnectQuestion(ICollection<String> belongTerms, ICollection<String> bellongingTerms) {
            BellongingTerms = bellongingTerms;
            BelongTerms = belongTerms;

        }
        public ConnectQuestion() { }
        public bool Validate()
        {
            //TODO: implement model validation
            return true;
        }
    }
}
