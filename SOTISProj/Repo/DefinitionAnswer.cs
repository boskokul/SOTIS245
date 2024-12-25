using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace SOTISProj.Repo
{
    public class DefinitionAnswer
    {
        public int Id { get; set; }
        [JsonIgnore]
        public Test Test { get; set; }
        public DefinitionQuestion Question { get; set; }
        public string AnsweredDefinition { get; set; }
        public bool IsCorrect { get; set; }
        [JsonIgnore]
        public TestSample TestSample { get; set; }
        public DefinitionAnswer() { }
       
    }
}
