using System.Collections.ObjectModel;

namespace SOTISProj.Repo
{
    public class ConnectAnswer
    {
        public int Id { get; set; }
        public Test Test { get; set; }
        public ConnectQuestion Question { get; set; }
        public List<Pair> ConnectedPairs { get; set; }
        public TestSample TestSample { get; set; }
        public ConnectAnswer() { }
        public bool Validate()
        {
            //TODO: implement model validation
            return true;
        }
    }
}
