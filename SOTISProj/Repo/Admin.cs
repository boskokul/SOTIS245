namespace SOTISProj.Repo
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public User user { get; set; }
        public bool Validate()
        {
            //TODO: implement model validation
            return true;
        }

    }
}
