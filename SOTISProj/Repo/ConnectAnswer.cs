using System.Collections.ObjectModel;

namespace SOTISProj.Repo
{
    public class ConnectAnswer
    {
        public int Id { get; set; }
        public Test Test { get; set; }
        public ConnectQuestion Question { get; set; }
        public ICollection<Pair> connectedPairs { get; set; }
        public int correctConnections { get; set; } //maybe out
        public ConnectAnswer() { }
        public bool Validate()
        {
            //TODO: implement model validation
            return true;
        }
    }
}
