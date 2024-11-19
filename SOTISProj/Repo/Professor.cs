namespace SOTISProj.Repo
{
    public class Professor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Subject Subject { get; set; }
        public bool Validate()
        {
            //TODO: implement model validation
            return true;
        }

    }
}
