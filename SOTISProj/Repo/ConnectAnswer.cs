using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace SOTISProj.Repo
{
    public class ConnectAnswer
    {
        public int Id { get; set; }
        [JsonIgnore]
        public Test Test { get; set; }
        public ConnectQuestion Question { get; set; }
        public List<Pair> ConnectedPairs { get; set; }
        [JsonIgnore]
        public TestSample TestSample { get; set; }
        public ConnectAnswer() { }
    }
}
