namespace SOTISProj.Repo
{
    public class Pair
    {
        public string TermLeft { get; set; }
        public string TermRight { get; set; }
        public bool IsCorrect { get; set; }
        public Pair() { }
        public bool Validate()
        {
            //TODO: implement model validation
            return true;
        }

    }
}
