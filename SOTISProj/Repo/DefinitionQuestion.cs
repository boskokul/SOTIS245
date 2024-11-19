namespace SOTISProj.Repo
{
    public class DefinitionQuestion
    {
        public int Id { get; set; }
        public string Term { get; set; }
        public DefinitionQuestion() { }
        public bool Validate()
        {
            //TODO: implement model validation
            return true;
        }

    }
}
