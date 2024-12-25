namespace SOTISProj.Repo
{
    public class DefinitionQuestion
    {
        public int Id { get; set; }
        public string Term { get; set; }
        public DefinitionQuestion() { }
        public DefinitionQuestion(string Term) {
            this.Term = Term;
        }
    }
}
